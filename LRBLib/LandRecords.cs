
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

            app.ContactPerson.dob = contactPerson.dob;
            app.ContactPerson.email = contactPerson.email;
            app.ContactPerson.employer_address = contactPerson.employer_address;
            app.ContactPerson.employer_name = contactPerson.employer_name;
            app.ContactPerson.telephone = contactPerson.telephone;
            app.ContactPerson.home_town = contactPerson.home_town;
            app.ContactPerson.lga = contactPerson.lga;
            app.ContactPerson.first_name = contactPerson.first_name;
            app.ContactPerson.middle_name = contactPerson.middle_name;
            app.ContactPerson.MobileNo = contactPerson.MobileNo;
            app.ContactPerson.occupation = contactPerson.occupation;
            app.ContactPerson.OfficeNo = contactPerson.OfficeNo;
            app.ContactPerson.corporate_name = contactPerson.corporate_name;
            app.ContactPerson.state_of_origin = contactPerson.state_of_origin;
            app.ContactPerson.surname = contactPerson.surname;
            
            
            uow.LandApplicationRepository.Update(app);

            var errors = uow.LandApplicationRepository.context.GetValidationErrors();
            uow.Save();
        }

        public static void SaveApplication(int appId, Property primaryProperty)
        {
            UnitOfWork uow = new UnitOfWork();
            var app = uow.LandApplicationRepository.GetByID(appId);

            app.PrimaryProperty.size_of_property = primaryProperty.size_of_property;
            app.PrimaryProperty.capacity_of_ownership = primaryProperty.capacity_of_ownership;
            app.PrimaryProperty.Developed = primaryProperty.Developed;
            app.PrimaryProperty.land_use = primaryProperty.land_use;
            app.PrimaryProperty.duration_of_possession = primaryProperty.duration_of_possession;

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
