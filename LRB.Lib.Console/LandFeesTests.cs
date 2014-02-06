using LRB.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Console
{
    public class LandFeesTests
    {
        public void StartTests()
        {
            System.Console.WriteLine("Row 2, Column 8:" + LandFees.GetField(2, 8));
            var results = LandFees.Calculate_Consent_Fees(1991, 650, "HighValue");
            System.Console.WriteLine("Valuation fee for 650 sqkm bought in 1991: " + results["ValuationFee"]);
            System.Console.WriteLine("Open Market Value for 650 sqkm bought in 1991: " + results["OMV"]);
            System.Console.WriteLine("Maintainace Costs for 650 sqkm bought in 1991: " + results["MaintainanceCosts"]);
        }

        public void calculate_consent_fee_from_title_search()
        {
            //LandTitles.initialize();
            //var title = LandTitles.search_by_prk(LandTitles.search_for_individual_owners(surName: "mimiko").FirstOrDefault().prkNo);

            //System.Console.WriteLine("Initialize Calculation from title search \n ");

            //var title_date = Convert.ToDateTime(title.effdate);


            //var results = LandFees.Calculate_Consent_Fees(title_date.Year, float.Parse(title.areasize), "HighValue");
            var results = LandFees.Calculate_Consent_Fees(1978, 650, "HighValue", false, Enums.LandUse.Educational);

            System.Console.WriteLine("Valuation fee for 650 sqkm bought in 1991: " + results["ValuationFee"]);
            System.Console.WriteLine("Open Market Value for 650 sqkm bought in 1991: " + results["OMV"]);
            System.Console.WriteLine("HCol for 650 sqkm bought in 1991: " + results["HCOL"]);
            System.Console.WriteLine("Maintainance Costs for 650 sqkm bought in 1991: " + results["MaintainanceCosts"]);
            System.Console.WriteLine("Other for 650 sqkm bought in 1991: " + results["other"]);
            System.Console.WriteLine("BettermentValue for 650 sqkm bought in 1991: " + results["BettermentValue"]);
            System.Console.WriteLine("Consent Fee for 650 sqkm bought in 1991: " + results["ConsentFee"]);
            System.Console.WriteLine("Capital Gain Tax for 650 sqkm bought in 1991: " + results["CapitalGainsTax"]);

        }
    }
}
