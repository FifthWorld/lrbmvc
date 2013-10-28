using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.sola.webservices.casemanagement.extra;

namespace org.sola.services.boundary.wsclients
{
    public interface ICaseManagementService
    {
        void SetCredentials(string userName, string password);
        bool CheckConnection();
        applicationTO GetApplication(string id);
        partyTO SaveParty(partyTO party);
        sourceTO SaveSource(sourceTO source);
        applicationTO SaveApplication(applicationTO application);
        applicationTO CreateApplication(applicationTO application);
    }
}
