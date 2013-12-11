using System;
using System.Collections.Generic;
using DataAccess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB
{
    public class LandFees
    {
        static DataTable data { get; set; }

        public static bool isLoaded
        {
            get
            {
                return data != null;
            }
        }

        public static void init(string path = @"data.csv")
        {
            data = DataTable.New.ReadCsv(path);
        }

        public static string GetField(int row, int col)
        {
            if (!isLoaded)
                init();

            Row r = data.Rows.ToList()[row];
            return r.Values[col];
        }

        public static Dictionary<String, float> Calculate_Consent_Fees(int year, float landSize, string LandValue, bool developed = true, string use = "Commercial")
        {
            Dictionary<String, float> results = new Dictionary<string, float>();

            Row nowRow = getRow(DateTime.Now.Year);
            Row oldRow = getRow(year);

            float MaintainanceCosts = 0;
            float OMV = float.Parse(nowRow[LandValue]) * landSize;
            float HCol = float.Parse(oldRow[LandValue]) * landSize;
            float SurvFee = float.Parse(oldRow["SurveyFee"]) * landSize;
            float TitleCost = float.Parse(oldRow["TitleCost"]);
            float BuildingPlan = developed ? float.Parse(oldRow["BuildingPlan"]) : 0;

            float LegalFee = OMV / 10;
            float ValuationFee = OMV / 100;

            for (int i = year; i < DateTime.Now.Year; i++)
            {
                var row = getRow(i);
                MaintainanceCosts += float.Parse(row["MaintainanceCost"]) * landSize;
            }

            float other = HCol + SurvFee + TitleCost + BuildingPlan + LegalFee + ValuationFee + MaintainanceCosts;
            float BettermentValue = OMV - other;
            float CapitalGainsTax = developed ? BettermentValue / 10 : BettermentValue / 2;
            float ConsentFee = developed ? BettermentValue / 20 : BettermentValue / 10;


            results["OMV"] = OMV;
            results["HCOL"] = HCol;
            results["SurveyFee"] = SurvFee;
            results["TitleCost"] = TitleCost;
            results["BuildingPlan"] = BuildingPlan;
            results["LegalFee"] = LegalFee;
            results["ValuationFee"] = ValuationFee;
            results["MaintainanceCosts"] = MaintainanceCosts;
            results["BettermentValue"] = BettermentValue;
            results["CapitalGainsTax"] = CapitalGainsTax;
            results["ConsentFee"] = ConsentFee;
            results["other"] = other;
            results["Total"] = ConsentFee + CapitalGainsTax;

            //results["ApplicationForm"] = getApplicationForm(use);
            //results["ProcessingFee"] = getProcessingFee(use, landSize);
            //results["LandManagementFee"] = getLandManagementFee(landSize);
            //results["LandUseCharge"] = getLandUseCharge(landSize, developed);
            //results["StampDuty"] = getStampDuty(landSize,use);


            return results;
        }

        private static float getStampDuty(float landSize, string use)
        {
            if (use == "Commercial" || use == "Endustrial" || use == "Educational")
            {
                
            }
            else if (use == "Residential")
            {
                
            }
            else
            {

            }
            throw new NotFiniteNumberException();
        }

        private static float getLandUseCharge(float landSize, bool developed)
        {
            throw new NotImplementedException();
        }

        private static float getLandManagementFee(float landSize)
        {
            throw new NotImplementedException();
        }

        private static float getProcessingFee(string use, float landSize)
        {
            throw new NotImplementedException();
        }

        private static float getApplicationForm(string use)
        {
            if (use == "Commercial" || use == "Endustrial" || use == "Educational")
            {
                
            }
            else if (use == "Residential")
            {

            }
            else
            {

            }
            throw new NotImplementedException();
        }

        private static Row getRow(int year)
        {
            string year_str = year.ToString();
            if (!isLoaded)
                init();

            foreach (var row in data.Rows.ToList())
            {
                if (row["Year"] == year_str)
                {
                    return row;
                }
            }
            return null;
        }
    }
}
