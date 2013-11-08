using LRB.Lib.Domain;
using LRB.Lib.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            LandRecordsTest lTests = new LandRecordsTest();
            lTests.CreateApplication_WithRequirement_And_SubmitToSola();

            //SearchTest searchTest = new SearchTest();
            //searchTest.init();

            //SolaTest stest = new SolaTest();

            //var appTO = stest.saveApplication();
            //System.Console.WriteLine("Application NR: " + appTO.nr);
            //System.Console.WriteLine("Application ID: " + appTO.id);


            //string status = stest.GetApplicationStatus(appTO.nr);
            //System.Console.WriteLine("Application status :" + status);

            System.Console.ReadKey();
        }
    }
}
