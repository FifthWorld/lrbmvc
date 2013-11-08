using LRB.Lib.Domain;
using org.sola.services.boundary.wsclients;
using org.sola.webservices.casemanagement.extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Console
{
    class SolaTest
    {
        public string GetApplicationStatus(string appNR)
        {
            ICaseManagementService caseMgmt = CasemanagementProxy.Instance;
            caseMgmt.SetCredentials("test", "test");
            var appTO = caseMgmt.GetApplication(appNR);
            return appTO.statusCode;
        }

        public applicationTO saveApplication()
        {
            ICaseManagementService caseMgmt = CasemanagementProxy.Instance;
            caseMgmt.SetCredentials("test", "test");
            applicationTO appTO = new applicationTO()
            {
                contactPerson = new partyTO()
                {
                    fathersLastName = "Ijaware",
                    fathersName = "Oluwatomiwa",
                    name="Oluwatomiwa",
                    email = "e911miri@gmail.com",
                    address = new addressTO()
                    {
                        description = "No 1, Fatai Kadiri street, gbagada, lagos",
                    },
                    dateOfBirth = DateTime.Now,
                    lastName = "Ijaware",
                    mobile = "07037290250",
                    phone = "07037290250",
                    typeCode = "naturalPerson"
                },
                serviceList = new serviceTO[]{
                    new serviceTO()
                    {
                        requestTypeCode="newCofO",
                        actionCode="lodge",
                        statusCode="lodged", 
                    }
                },
            };
            return caseMgmt.SaveApplication(appTO);
        }

        public string GetApplication_ViaTransactionId(string appNR)
        {
            ICaseManagementService caseMgmt = CasemanagementProxy.Instance;
            caseMgmt.SetCredentials("test", "test");
            var appTO = caseMgmt.GetApplicationViaTransactionId(appNR);
            return appTO.statusCode;
        }
    }
}
