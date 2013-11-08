
using LRB.Lib;
using LRB.Lib.Domain;
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
    public class SolaService
    {
        public static string username = "test";
        public static string password = "test";

        private int AppId;
        public SolaService(int appId)
        {
            AppId = appId;
        }

        public applicationTO SubmitToSola()
        {
            Application app = LandRecords.GetApplication(AppId);

            // initialize the case management service
            ICaseManagementService caseManagementService = CasemanagementProxy.Instance;
            caseManagementService.SetCredentials(username, password);

            var appTO = new applicationTO();

            appTO.contactPerson = getContactPerson();
            appTO.propertyList = getPropertyList();
            appTO.serviceList = getServiceList();
            appTO.sourceList = getSourceList();
            var solaAppTO = caseManagementService.SaveApplication(appTO);


            app.SolaId = solaAppTO.id;
            app.Status = solaAppTO.statusCode;
            return solaAppTO;
        }

        public string GetSolaStatus()
        {
            ICaseManagementService caseMgmt = CasemanagementProxy.Instance;
            caseMgmt.SetCredentials(username, password);
            var app = LandRecords.GetApplication(AppId);
            var solaApp = caseMgmt.GetApplication(app.SolaId);
            if (app.SolaId != null)
            {
                solaApp = caseMgmt.GetApplication(app.SolaId);
                return solaApp.nr;
            }
            return null;
        }

        public bool isComplete()
        {
            var app = LandRecords.GetApplication(AppId);

            return
                   app.UserId != null
                && app.ContactPerson.Firstname != null
                && app.ContactPerson.Surname != null
                && app.ContactPerson.MobileNo != null;
        }

        private sourceTO[] getSourceList()
        {
            var app = LandRecords.GetApplication(AppId);
            SolaDocService docService = new SolaDocService(username, password);
            var SourceList = new List<sourceTO>();
            foreach (var doc in app.Documents)
            {
                SourceList.Add(docService.makeSourceTO(doc.Content, doc.Extension, doc.Description, app.ContactPerson.FullName));

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
            var app = LandRecords.GetApplication(AppId);
            var person = new partyTO();
            var party = app.ContactPerson;
            if (party !=null)
            {
                person.name = party.Firstname;
                person.lastName = party.Surname;
                person.email = party.Email;
                person.alias = party.Firstname;
                person.dateOfBirth = party.DOB.GetValueOrDefault();

                person.typeCode = party.OrganizationName != null ? "nonNaturalPerson" : "naturalPerson";

                person.mobile = app.ContactPerson.MobileNo;
                person.preferredCommunicationCode = "phone";

                if (app.ContactPerson.ContactAddress != null)
                {
                    person.address = new addressTO()
                    {
                        description = party.ContactAddress.ToString()
                    };
                }

                //homeTownTypeCode = app.Party.homeTownTypeCode,
                //lgaTypeCode = app.Party.lgaTypeCode,
                //stateTypeCode = app.Party.stateCode,
                //titleTypeCode = app.Party.titleCode,
                //person.genderCode = app.ContactPerson.Gender;
                // person.occupationTypeCode = app.ContactPerson.Occupation;
                //person.rightHolder = true;
                //person.employerAddress = app.ContactPerson.EmployerAddress;
                //person.employerName = app.ContactPerson.EmployerName;

                return person;
            }
            return null;
            
        }
    }
}
