using LRB;
using LRBMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRBMvc.Controllers
{
    public class ConsentFeesController : Controller
    {
        //
        // GET: /ConsentFees/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calculate()
        {            
            var dict = new Dictionary<string, string>();
            dict["HighValue"]="High Value";
            dict["MediumValue"]= "Medium Value";
            dict["BaseValue"] = "Base Value";
            
            List<int> years = new List<int>();
            for (int i = 1978; i<= 2013; i++)
            {
                years.Add(i);
            }

            ViewBag.Years = years;
            ViewBag.BandValues = dict;
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(ConsentParams consentParams)
        {
            var dict = new Dictionary<string, string>();
            dict["HighValue"] = "High Value";
            dict["MediumValue"] = "Medium Value";
            dict["BaseValue"] = "Base Value";

            List<int> years = new List<int>();
            for (int i = 1978; i <= 2013; i++)
            {
                years.Add(i);
            }

            ViewBag.Years = years;
            ViewBag.BandValues = dict;
            var base_dir = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\data.csv";
            LandFees.init(base_dir);
            var BaseValue = LandFees.Calculate_Consent_Fees(consentParams.Year, consentParams.LandSize, consentParams.LandValue);
            ViewBag.BaseValue = BaseValue;
            try
            {
                
            }
            catch
            {
                
            }
            return View();
        }
    }
}
