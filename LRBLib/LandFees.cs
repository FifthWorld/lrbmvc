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

        public static Dictionary<String, String> Calculate_C_of_O_Charges(int year, float LandSize, string landValue, bool developed, LandUse use)
        {
            var formatter = new System.Globalization.CultureInfo("HA-LATN-NG");
            formatter.NumberFormat.CurrencySymbol = "₦";
            float application_form = getApplicationForm(use);
            float processing_fee = getProcessingFee(use, LandSize);
            float landManagement_fee = getLandManagementFee(LandSize);
            float landUse_Charge = getLandUseCharge(LandSize, developed);
            float digitization = getStateWideDigitization(use);
            float sltr = getSLTR(use);
            float total = application_form + processing_fee + landManagement_fee + landUse_Charge;
            Dictionary<string, string> result = new Dictionary<string, string>();
            result["Application_Form"] = String.Format(formatter, "{0:C}", application_form);
            result["Processing_Fee"] = String.Format(formatter, "{0:C}", processing_fee);
            result["LandManagement_Fee"] = String.Format(formatter, "{0:C}", landManagement_fee);
            result["LandUse_Charge"] = String.Format(formatter, "{0:C}", landUse_Charge);
            result["StateWideDigitization_Charge"] = "NA"; //String.Format(formatter, "{0:C}", digitization);
            result["SLTR"] = "NA";
            result["Total"] = String.Format(formatter, "{0:C}", total); ;
            return result;
        }

        public static Dictionary<string, string> as_currency(Dictionary<string, float> incoming)
        {
            var formatter = new System.Globalization.CultureInfo("HA-LATN-NG");
            formatter.NumberFormat.CurrencySymbol = "₦";
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in incoming.Keys)
            {
                result[item] = String.Format(formatter, "{0:C}", incoming[item]);
            }
            return result;
        }
        #endregion

        #region Land Charges

        private static float getForm(LandUse use)
        {
            float result;
            if (use == LandUse.Commercial || use == LandUse.Industrial || use == LandUse.Educational)
            {
                result = 10000;
            }
            else if (use == LandUse.Residential)
            {
                result = 5000;
            }
            else
            {
                result = 2500;
            }
            return result;
        }

        private static float getPremium(LandUse use, float landSize)
        {
            float premium;
            if (use == LandUse.Commercial || use == LandUse.Industrial || use == LandUse.Educational)
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

        private static float getProcessingFee(LandUse use, float landSize)
        {
            float result = 0f;
            if (use == LandUse.Commercial || use == LandUse.Industrial || use == LandUse.Educational)
            {
                result += 50000;
                if (landSize > 1000 && landSize <= 4000)
                {
                    if (landSize > 4000)
                    {
                        result += 3000 * 20;
                        result += (landSize - 4000) * 40;
                    }
                    else
                    {
                        result += (landSize - 1000) * 20;
                    }
                }
            }
            else if (use == LandUse.Residential)
            {
                result += 20000;
                if (landSize > 1000 && landSize <= 4000)
                {
                    if (landSize > 4000)
                    {
                        result += 3000 * 15;
                        result += (landSize - 4000) * 30;
                    }
                    else
                    {
                        result += (landSize - 1000) * 15;
                    }
                }
            }
            else
            {
                result += (5000 * landSize) / 10000;
            }
            return result;
        }

        private static float getDevelopmentCharge(string use)
        {
            throw new NotImplementedException();
        }

        private static float getLandManagementFee(float landSize)
        {
            return landSize > 1000 ? 7500 + (landSize - 1000) * 1 : 7500;
        }

        private static float getLandUseCharge(float landSize, bool developed)
        {
            float baseValue = developed ? 7500 : 5000;
            float multiplier = developed ? 5 : 1;
            return landSize > 1000 ? baseValue + (landSize - 1000) * multiplier : baseValue;
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

        private static float getSLTR(LandUse use)
        {
            float result = 0f;
            if (use == LandUse.Commercial || use == LandUse.Industrial || use == LandUse.Educational)
            {
                result = 50000;
            }
            else if (use == LandUse.Residential)
            {
                result = 30000;
            }
            else
            {
                result = 20000;
            }
            return result;
        }

        private static float getStampDuty(float landSize, string use)
        {
            if (use == "Commercial" || use == "Industrial" || use == "Educational")
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

        private static float getApplicationForm(LandUse use)
        {
            if (use == LandUse.Commercial || use == LandUse.Industrial || use == LandUse.Educational)
            {
                return 10000;
            }
            else if (use == LandUse.Residential)
            {
                return 5000;
            }
            else
            {
                return 2500;
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
            return (LandUse)Enum.Parse(typeof(LandUse), app.PrimaryProperty.LandUse);
        }

        public static bool getLandDevelopment(Lib.Domain.Application app)
        {
            return app.PrimaryProperty.Development == null ? false : true;
        }
    }
        #endregion
}
