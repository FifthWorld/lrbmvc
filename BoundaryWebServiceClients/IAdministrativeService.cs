using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.sola.webservices.administrative.extra;

namespace org.sola.services.boundary.wsclients
{
    public interface IAdministrativeService
    {
        void SetCredentials(string userName, string password);
        bool CheckConnection();
        baUnitTO GetBaUnitById(string baUnitId);
        baUnitTO SaveBaUnit(string serviceId, baUnitTO baUnit);
        baUnitTO CreateBaUnit(string serviceId, baUnitTO baUnit);
    }
}
