
using LRB.Lib.Domain;
using LRB.Lib.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib
{
    public static class LandRecords
    {
        public static IEnumerable<Application> GetApplications(string username)
        {
            UnitOfWork uow = new UnitOfWork();
            return uow.LandApplicationRepository.Get(u => u.UserId == username);
        }

        public static Application GetApplication(int AppId)
        {
            UnitOfWork uow = new UnitOfWork();
            return uow.Context.Applications.Where(u => u.Id == AppId).Include("Parties").Include("Properties").SingleOrDefault();
        }

        public static void SaveApplication(Application app)
        {
            UnitOfWork uow = new UnitOfWork();
            uow.LandApplicationRepository.Update(app);
            uow.Save();
        }

        public static Party GetContactPerson(int appId)
        {
            UnitOfWork uow = new UnitOfWork();
            return uow.Context.Parties.Where(p => p.PartyType == "ContactPerson" && p.Application.Id == appId).FirstOrDefault();
        }

        public static Property GetPrimaryProperty(int propertyId)
        {
            UnitOfWork uow = new UnitOfWork();
            return uow.Context.Properties.Where(p => p.Id == propertyId).FirstOrDefault();
        }

        public static void SaveApplication(int appId, Party contactPerson)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.Context.Applications.Where(p => p.Id == appId).Include("Parties").FirstOrDefault();//GetApplication(appId);
            var party = app.Parties.Where(p => p.PartyType == "ContactPerson").FirstOrDefault();
            if (party == null)
            {
                party = new Party() { PartyType = "ContactPerson" };
                app.Parties.Add(party);
            }
            party.DOB = contactPerson.DOB;
            party.Email = contactPerson.Email;
            party.EmployerAddress = contactPerson.EmployerAddress;
            party.EmployerName = contactPerson.EmployerName;
            party.Phone2 = contactPerson.Phone2;
            party.HomeTown = contactPerson.HomeTown;
            party.LGA = contactPerson.LGA;
            party.Firstname = contactPerson.Firstname;
            party.Middlename = contactPerson.Middlename;
            party.MobileNo = contactPerson.MobileNo;
            party.Occupation = contactPerson.Occupation;
            party.OfficeNo = contactPerson.OfficeNo;
            party.OrganizationName = contactPerson.OrganizationName;
            //party.StateofOrigin = contactPerson.StateofOrigin;
            party.Surname = contactPerson.Surname;

            party.LGA = contactPerson.ILGA;
            party.Town = contactPerson.Town;
            party.IState = contactPerson.IState;
            party.Street = contactPerson.Street;

            uow.Context.Applications.Attach(app);
            uow.Context.SaveChanges();
        }

        public static void SaveApplication(int appId, Property primaryProperty)
        {
            UnitOfWork uow = new UnitOfWork();

            var property = uow.Context.Properties.Where(p => p.Application.Id == appId).FirstOrDefault();
            property.LandSize = primaryProperty.LandSize;
            property.LandSizeUnit = primaryProperty.LandSizeUnit;
            property.CapacityofOwnership = primaryProperty.CapacityofOwnership;
            property.Development = primaryProperty.Development;
            property.LandUse = primaryProperty.LandUse;
            property.PeriodofPossession_months = primaryProperty.PeriodofPossession_months;
            property.PeriodofPossession_years = primaryProperty.PeriodofPossession_years;

            property.LGA = primaryProperty.LGA;
            property.Town = primaryProperty.Town;
            property.State = primaryProperty.State;
            property.Street = primaryProperty.Street;

            uow.Context.SaveChanges();
        }

        public static void SaveApplication(int appId, DocumentManager docManager)
        {
            UnitOfWork uow = new UnitOfWork();

            var dm = uow.Context.DocumentManagers.Where(p => p.Application.Id == appId).FirstOrDefault();
            dm.DevelopmentLevy = docManager.DevelopmentLevy;
            dm.DevelopmentLevy_RecieptNumber = docManager.DevelopmentLevy_RecieptNumber;
            dm.EvidenceOfOwnership = docManager.EvidenceOfOwnership;
            dm.EvidenceType = docManager.EvidenceType;
            dm.Passport = docManager.Passport;
            dm.SurveyPlan = docManager.SurveyPlan;
            dm.SurveyPlan_ApprovalDate = docManager.SurveyPlan_ApprovalDate;
            dm.SurveyPlan_Number = docManager.SurveyPlan_Number;

            uow.Context.SaveChanges();
        }
        public static void SaveApplication(int appId, Document document)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);
            app.Documents.Add(document);

            uow.LandApplicationRepository.Update(app);
            uow.Save();
        }

        public static void SaveApplication(int appId, Optional document)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);
            if (app.OptionalRequirement == null)
            {
                app.Optionals.Add(new Optional());
            }
            app.OptionalRequirement.EmployerName = document.EmployerName;
            app.OptionalRequirement.HomeTown = document.HomeTown;
            app.OptionalRequirement.LGA = document.LGA;
            app.OptionalRequirement.Occupation = document.Occupation;
            app.OptionalRequirement.State = document.State;
            app.OptionalRequirement.Street = document.Street;
            app.OptionalRequirement.StateofOrigin = document.StateofOrigin;
            app.OptionalRequirement.Town = document.Town;

            uow.LandApplicationRepository.Update(app);
            uow.Save();
        }

        public static void SubmitApplication(int appId)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);
            app.SubmissionDate = DateTime.Now;
            app.SubmittedbyApplicant = true;
            uow.LandApplicationRepository.Update(app);
            uow.Save();
        }

        public static void UpdateStatus(int appId, string solaStatus, string solaId, String solaNR)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);
            app.Status = solaStatus;
            app.SolaId = solaId;
            app.SolaNR = solaNR;

            uow.LandApplicationRepository.Update(app);
            uow.Save();
        }

        public static Application NewApplication()
        {
            var user = SimpleSecurity.WebSecurity.GetCurrentUser();
            Application app = new Application()
            {
                StartDate = DateTime.Now,
                UserId = user.UserName,
                ApplicationType = "Individual",
                Status = "Incomplete"
            };

            UnitOfWork uow = new UnitOfWork();
            uow.LandApplicationRepository.Insert(app);
            uow.Save();

            return app;
        }

        public static Application CreateApplication(Requirement requirement)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = new Application();
            app.ApplicationType = requirement.applicationType==null ? "Individual": requirement.applicationType;


            Property prop = new Property()
            {
                LandSize = requirement.landSize,
                LandSizeUnit = requirement.landSizeUnit,
                LandUse = requirement.landUse,
                Development = "Undeveloped",
                CapacityofOwnership = "Inheritance",
                PeriodofPossession_months="0",
                PeriodofPossession_years="0",

                Addresses = new List<Address>()
                {
                    new Address(){
                        AddressType="PropertyLocation"
                    }
                }
            };

            DocumentManager docManager = new DocumentManager();
            app.requirementDocuments.Add(docManager);
            app.Properties.Add(prop);
            uow.Context.Applications.Add(app);
            


            app.Optionals.Add(new Optional() { });

            uow.Context.SaveChanges();
            uow.Save();

            return app;
        }

        public static Application CreateApplication(Requirement requirement, string username)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = new Application(username);
            app.ApplicationType = requirement.applicationType == null ? "Individual" : requirement.applicationType;


            Property prop = new Property()
            {
                LandSize = requirement.landSize,
                LandSizeUnit = requirement.landSizeUnit,
                LandUse = requirement.landUse,
                Development = "Undeveloped",
                CapacityofOwnership = "Inheritance",
                PeriodofPossession_months = "0",
                PeriodofPossession_years = "0",

                Addresses = new List<Address>()
                {
                    new Address(){
                        AddressType="PropertyLocation"
                    }
                }
            };

            DocumentManager docManager = new DocumentManager();
            app.requirementDocuments.Add(docManager);
            app.Properties.Add(prop);
            uow.Context.Applications.Add(app);



            app.Optionals.Add(new Optional() { });

            uow.Context.SaveChanges();
            uow.Save();

            return app;
        }


        public static IEnumerable<Document> GetDocuments(int appId)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);
            var documents = from doc in app.Documents select new { Title = doc.FileName, Type = doc.DocumentType };
            return documents as IEnumerable<Document>;
        }
        public static DocumentManager GetDocumentManager(int appId)
        {
            UnitOfWork uow = new UnitOfWork();
            var dm = uow.Context.DocumentManagers.Where(p => p.Application.Id == appId).FirstOrDefault();
            return dm;
        }

        public static String SaveDocument(int appId, Document doc)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);
            app.Documents.Add(doc);
            uow.LandApplicationRepository.Update(app);
            uow.Save();
            return doc.Id.ToString();
        }

        public static void Remove(int appId)
        {
            UnitOfWork uow = new UnitOfWork();
            
        }
    }
}
