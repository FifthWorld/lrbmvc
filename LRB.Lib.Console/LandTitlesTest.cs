using LRB.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Console
{
    class LandTitlesTest
    {
        public void search_for_mimiko()
        {
            LandTitles.initialize();
            var props = LandTitles.search_for_individual_owners("mimiko");
            System.Console.WriteLine("No of Mimiko's property: " + props.Count());
        }

        public void search_for_orim()
        {
            LandTitles.initialize();
            var props = LandTitles.search_for_property("orim");
            System.Console.WriteLine("No of Orims's property: " + props.Count());
        }
    }
}
