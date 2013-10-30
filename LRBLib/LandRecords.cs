
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
            return uow.LandApplicationRepository.Get(u => u.Id == AppId).SingleOrDefault();
        }

        public static void SaveApplication(Application app)
        {
            UnitOfWork uow = new UnitOfWork();
            uow.LandApplicationRepository.Update(app);
            uow.Save();
        }

        public static void SaveApplication(int appId, Party contactPerson)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);

            app.ContactPerson.DOB = contactPerson.DOB;
            app.ContactPerson.Email = contactPerson.Email;
            app.ContactPerson.EmployerAddress = contactPerson.EmployerAddress;
            app.ContactPerson.EmployerName = contactPerson.EmployerName;
            app.ContactPerson.HomeNo = contactPerson.HomeNo;
            app.ContactPerson.HomeTown = contactPerson.HomeTown;
            app.ContactPerson.LGA = contactPerson.LGA;
            app.ContactPerson.Firstname = contactPerson.Firstname;
            app.ContactPerson.Middlename = contactPerson.Middlename;
            app.ContactPerson.MobileNo = contactPerson.MobileNo;
            app.ContactPerson.Occupation = contactPerson.Occupation;
            app.ContactPerson.OfficeNo = contactPerson.OfficeNo;
            app.ContactPerson.OrganizationName = contactPerson.OrganizationName;
            app.ContactPerson.StateofOrigin = contactPerson.StateofOrigin;
            app.ContactPerson.Surname = contactPerson.Surname;
            
            
            uow.LandApplicationRepository.Update(app);

            var errors = uow.LandApplicationRepository.context.GetValidationErrors();
            uow.Save();
        }

        public static void SaveApplication(int appId, Property primaryProperty)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);

            app.PrimaryProperty.LandSize = primaryProperty.LandSize;
            app.PrimaryProperty.LandSizeUnit = primaryProperty.LandSizeUnit;
            app.PrimaryProperty.CapacityofOwnership = primaryProperty.CapacityofOwnership;
            app.PrimaryProperty.Developed = primaryProperty.Developed;
            app.PrimaryProperty.LandUse = primaryProperty.LandUse;
            app.PrimaryProperty.PeriodofPossession = primaryProperty.PeriodofPossession;

            uow.LandApplicationRepository.Update(app);
            uow.Save();
        }

        public static void SubmitApplication(int appId)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);
            app.Status = "Lodged";
            app.SubmissionDate = DateTime.Today;
            app.SubmittedbyApplicant = true;
            uow.LandApplicationRepository.Update(app);
            uow.Save();
        }

        public static Application NewApplication()
        {
            var user = SimpleSecurity.WebSecurity.GetCurrentUser();
            Application app = new Application() { 
                StartDate = DateTime.Now, 
                UserId = user.UserName, ApplicationType = "Certificate of Occupancy" };

            UnitOfWork uow = new UnitOfWork();
            uow.LandApplicationRepository.Insert(app);
            uow.Save();

            return app;
        }

        public static IEnumerable<Document> GetDocuments(int appId)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);
            return app.Documents;
        }

        public static void Remove(int appId)
        {
            UnitOfWork uow = new UnitOfWork();
            uow.LandApplicationRepository.Delete(uow.LandApplicationRepository.GetByID(appId));
            uow.Save();
        }
    }
}
