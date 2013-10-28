using ApplicationLibrary.Data;
using org.sola.services.boundary.wsclients;
using org.sola.webservices.casemanagement.extra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLibrary
{
    public class SolaApplicationService
    {
        private SolaDbContext _context;

        public SolaDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        private Application _app;

        public static string username = "test";
        public static string password = "test";

        public Application app
        {
            get { return _app; }
            set { _app = value; }
        }


        /*
         * This constructor instantiates the DbContext that is used during the
         * lifetime of this application
         * This constructor should be used when a new form is created
         */
        public SolaApplicationService()
        {
            Context = new SolaDbContext();
            app = new Application();
            app.StartDate = DateTime.Today;
            app.Parties.Add(new Party() { PartyType = "ContactPerson" });
        }

        public SolaApplicationService(int ApplicationId)
        {
            Context = new SolaDbContext();
            var getApp = Context.Applications.Where(p => p.Id == ApplicationId);

            if (getApp != null)
            {
                app = getApp.FirstOrDefault();
            }
            else
            {
                throw new Exception("No application is found with that ID...cheers");
            }
        }

        public Application getApplicationById(int Id)
        {
            return Context.Applications.Where(p => p.Id == Id).FirstOrDefault();
        }

        public applicationTO SubmitToSola()
        {            
            save();

            if (isComplete())
            {
                // initialize the case manegement service
                ICaseManagementService caseManagementService = CasemanagementProxy.Instance;
                caseManagementService.SetCredentials(username, password);

                var appTO = new applicationTO();
                var contactPerson = getContactPerson();
                appTO.contactPerson = contactPerson;

                appTO.propertyList = getPropertyList();
                appTO.serviceList = getServiceList();
                appTO.sourceList = getSourceList();

                var solaAppTO = caseManagementService.SaveApplication(appTO);
                app.SolaId = solaAppTO.nr;
                app.Status = solaAppTO.statusCode;
                
                return solaAppTO;
            }

            return null;
        }

        public bool isComplete()
        {
            return
                   app.UserId != null
                && app.ContactPerson.Firstname != null
                && app.ContactPerson.Surname != null
                && app.ContactPerson.MobileNo != null;
        }

        private sourceTO[] getSourceList()
        {
            SolaDocService docService = new SolaDocService(username, password);
            var SourceList = new List<sourceTO>();
            foreach (var doc in app.Documents)
            {
                SourceList.Add(docService.makeSourceTO(doc.Content, doc.Extension, doc.Description, app.ContactPerson.getName));

            }
            return SourceList.ToArray();
        }

        private serviceTO[] getServiceList()
        {
            return new serviceTO[]
            {
                new serviceTO(){
                    requestTypeCode="newCofO",
                    actionCode="lodge",
                    statusCode="lodged",                    
                }
            };
        }

        private applicationPropertyTO[] getPropertyList()
        {
            return null;
        }

        private partyTO getContactPerson()
        {
            var person = new partyTO();
            person.fathersName = app.ContactPerson.Firstname;
            person.lastName = app.ContactPerson.Surname;
            person.email = app.ContactPerson.Email;
            person.corporateName = app.ContactPerson.OrganizationName;
            person.alias = app.ContactPerson.Firstname;
            person.dateOfBirth = app.ContactPerson.DOB.GetValueOrDefault();
            //person.genderCode = app.ContactPerson.Gender;
            person.typeCode = app.ContactPerson.OrganizationName != null ? "nonNaturalPerson" : "naturalPerson";
            //homeTownTypeCode = app.Party.homeTownTypeCode,                
            //lgaTypeCode = app.Party.lgaTypeCode,
            //stateTypeCode = app.Party.stateCode,
            //titleTypeCode = app.Party.titleCode,
            person.mobile = app.ContactPerson.MobileNo;
            // person.occupationTypeCode = app.ContactPerson.Occupation;
            // person.rightHolder = true;


            person.address = new addressTO()
            {
                description = "tomcat avenue, smoky way, wandechris, Nigeria"
                // description = app.ContactPerson.Addresses[0],
            };
            // person.preferredCommunicationCode = "phone";
            person.employerAddress = app.ContactPerson.EmployerAddress;
            person.employerName = app.ContactPerson.EmployerName;

            return person;
        }

        public string save()
        {
            if (app.UserId == null)
            {
                app.Status = "No User";
            }
            else
            {
                if (app.EntityState == EntityState.Detached)
                {
                    Context.AddToApplications(app);
                }

                app.Status = "Incomplete";
                Context.SaveChanges();
                return app.EntityKey.ToString();
            }

            return null;
        }
    }
}
