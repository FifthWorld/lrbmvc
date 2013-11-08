using org.sola.services.boundary.wsclients;
using org.sola.webservices.administrative.extra;
using org.sola.webservices.search.extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Console
{
    class SearchTest
    {
        public void init()
        {
            System.Console.WriteLine("Starting tests...");
            IAdministrativeService administrativeService = AdministrativeServiceProxy.Instance;
            administrativeService.SetCredentials("test", "test");
            System.Console.WriteLine("AdministrativeService.CheckConnection = " + administrativeService.CheckConnection());
            baUnitTO baUnit = administrativeService.GetBaUnitById("2965019"); // Valid BA Unit Id required!!
            if (baUnit != null)
            {
                System.Console.WriteLine("AdministrativeService.GetBaUnitById = " + baUnit.nameFirstpart + "/" + baUnit.nameLastpart + " - " + baUnit.rowVersion);

                // To test the save you need to provide a service id that is referenced by a pending transaction.transaction
                //baUnit.nameFirstpart = "2";
                //baUnit = administrativeService.SaveBaUnit("26cf9f4e-8081-475e-831e-275967754c98", baUnit);
                //Console.WriteLine("AdministrativeService.SaveBaUnit = " + baUnit.nameFirstpart + "/" + baUnit.nameLastpart + " - " + baUnit.rowVersion);
            }

            ISearchService searchService = SearchServiceProxy.Instance;
            searchService.SetCredentials("test", "test");
            System.Console.WriteLine("SearchService.CheckConnection = " + searchService.CheckConnection());
            userSearchResultTO[] activeUsers = searchService.GetActiveUsers();
            int size = activeUsers == null ? 0 : activeUsers.Length;
            System.Console.WriteLine("Num of Active Users = " + size);
            System.Console.WriteLine("*** Press enter to close ***");


            System.Console.ReadLine();
        }
    }
}
