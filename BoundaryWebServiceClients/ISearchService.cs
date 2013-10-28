using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.sola.webservices.search.extra;

namespace org.sola.services.boundary.wsclients
{
    public interface ISearchService
    {
        void SetCredentials(string userName, string password);
        bool CheckConnection();
        userSearchResultTO[] GetActiveUsers(); 

    }
}
