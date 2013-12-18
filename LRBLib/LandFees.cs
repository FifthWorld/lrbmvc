using System;
using System.Collections.Generic;
using DataAccess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRB.Enums;

namespace LRB
{
    public class LandFees
    {
        #region Initialization
        static DataTable data { get; set; }

        public static bool isLoaded
        {
            get
            {
                return data != null;
            }
        }

        #endregion

        #region Consent Fees Calculation
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

        private static Dictionary<String, float> calculator(int year, float landSize, string LandValue, bool developed, LandUse use)
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
            results["Premium"] = getPremium(use, landSize);
            results["ProcessingFee"] = getProcessingFee(use, landSize);
            //results["DevelopmentCharge"] = getDevelopmentCharge(use);
            results["LandManagementFee"] = getLandManagementFee(landSize);
            results["LandUseCharge"] = getLandUseCharge(landSize, developed);
            results["StateWideDigitization"] = getStateWideDigitization(use);
            //results["StampDuty"] = getStampDuty(landSize,use);


            return results;
        }
        
        public static Dictionary<String, float> Calculate_Consent_Fees(int year, float landSize, string LandValue, bool developed, LandUse use)
        {
            return calculator(year, landSize, LandValue, developed, use);
        }

        public static Dictionary<String, float> Calculate_Consent_Fees(int year, float landSize, string LandValue)
        {
            return calculator(year, landSize, LandValue, true, LandUse.Commercial);
        }

        #endregion

        #region Land Charges

        private static float getPremium(LandUse use, float landSize)
        {
            float premium;
            if (use==LandUse.Commercial || use==LandUse.Industrial || use==LandUse.Educational)
            {
                premium = 400 * landSize;
            }
            else if (use == LandUse.Residential)
            {
                premium = 350 * landSize;
            }
            else
            {
                premium = 200 * landSize;
            }
            return premium;
        }

        private static float getStateWideDigitization(LandUse use)
        {
            float digitizationFees = 0f;
            if (use == LandUse.Commercial || use == LandUse.Industrial || use == LandUse.Educational)
            {
                digitizationFees = 10000;
            }
            else if (use == LandUse.Residential)
            {
                digitizationFees = 7500;
            }
            else
            {
                digitizationFees = 5000;
            }
            return digitizationFees;
        }

        private static float getDevelopmentCharge(string use)
        {
            throw new NotImplementedException();
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
            float baseValue = developed ? 7500: 5000;
            float multiplier = developed ? 5 : 1;
            return landSize > 1000 ? baseValue + (landSize - 1000) * multiplier : 1000;
        }

        private static float getLandManagementFee(float landSize)
        {
            return landSize > 1000 ? 7500 + (landSize - 1000) * 1 : 1000;
        }

        private static float getProcessingFee(LandUse use, float landSize)
        {
            float procFee = 0f, temp = 0f;
            if (use == LandUse.Commercial || use == LandUse.Industrial || use == LandUse.Educational)
            {
                procFee += landSize > 1000 ? 50000 : 50000;
                temp = landSize - 1000;
                procFee += temp > 4000 ? 4000 * 20 + (temp - 4000) * 40 : temp * 20;                
            }
            else if (use == LandUse.Residential)
            {
                procFee += landSize > 1000 ? 20000 : 20000;
                temp = landSize - 1000;
                procFee += temp > 4000 ? 4000 * 15 + (temp - 4000) * 30 : temp * 15; 
            }
            else
            {
                procFee += 5000 * landSize;
            }
            return procFee;            
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


        #endregion
        
        #region Front End Land Charges
        public static string getLandValue(Lib.Domain.Application app)
        {
            return "HighValue";
        }

        public static LandUse getLandUse(Lib.Domain.Application app)
        {
            return (LandUse)Enum.Parse(typeof(LandUse), "Commercial");
        }

        public static bool getLandDevelopment(Lib.Domain.Application app)
        {
            return app.PrimaryProperty.Development == null ? false : true;
        }
    }
        #endregion
}
