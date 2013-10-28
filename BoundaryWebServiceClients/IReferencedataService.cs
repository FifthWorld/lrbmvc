using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.sola.webservices.referencedata.extra;

namespace org.sola.services.boundary.wsclients
{
    public interface IReferencedataService
    {
        void SetCredentials(string userName, string password);
        bool CheckConnection();
        applicationActionTypeTO[] GetApplicationStatusTypes();
        communicationTypeTO[] GetCommunicationTypes();
        genderTypeTO[] GetGenderTypes();
        partyRoleTypeTO[] GetPartyRoleTypes();
        partyTypeTO[] GetPartyTypes();
        registrationStatusTypeTO[] GetRegistrationTypes();
        requestTypeTO[] GetRequestTypes();
        sourceTypeTO[] GetSourceTypes();
        capacityTypeTO[] GetCapacityTypes();
        homeTownTypeTO[] GetHomeTownTypes();
        lgaTypeTO[] GetLgaTypes();
        occupationTypeTO[] GetOccupationTypes();
        propertyTypeTO[] GetPropertyTypes();
        stateTypeTO[] GetStateTypes();
        titleTypeTO[] GetTitleTypes();
        useTypeTO[] GetUseTypes();

    }
}
