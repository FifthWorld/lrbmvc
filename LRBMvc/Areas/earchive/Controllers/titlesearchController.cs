using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using LRB.Legacy.Entities;
using LRB.Legacy;
using LRB;

namespace LRBMvc.Areas.earchive.Controllers
{
    public class titlesearchController : Controller
    {
        //
        // GET: /earchive/titlesearch/

        public ActionResult Index(int? page,
            String town = "", String lga = "", String street = "", string location = "",
            string firstname = "", string surname = "", string middlename = "", string owner = "",
            string name = "", string industry = "",
            string prkNo = "")
        {
            IEnumerable<SearchItem> result;
            if (location == "true")
            {
                result = LandTitles.search_for_property(town, lga, street);
            }
            else if (owner == "true")
            {
                result = LandTitles.search_for_individual_owners(surname, firstname, middlename);
            }
            else if (industry == "true")
            {
                result = LandTitles.search_for_corparate_properties(name);
            }
            else if (prkNo != "")
            {
                return RedirectToAction("Details", new { prkNo = prkNo });
            }
                else
            {
                result = LandTitles.search_for_property();
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /earchive/titlesearch/Details/5

        public ActionResult Details(string prkNo)
        {
            var prop = LandTitles.search_by_prk(prkNo);
            var title_date = Convert.ToDateTime(prop.effdate);
            var base_dir = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\data.csv";
            LandFees.init(base_dir);
            var HighValue = LandFees.as_currency(LandFees.Calculate_Consent_Fees(title_date.Year, float.Parse(prop.areasize), "HighValue"));
            var MediumValue = LandFees.as_currency(LandFees.Calculate_Consent_Fees(title_date.Year, float.Parse(prop.areasize), "MediumValue"));
            var BaseValue = LandFees.as_currency(LandFees.Calculate_Consent_Fees(title_date.Year, float.Parse(prop.areasize), "BaseValue"));

            ViewBag.HighValue = HighValue;
            ViewBag.MediumValue = MediumValue;
            ViewBag.BaseValue = BaseValue;
            
            return View(prop);
        }

        //
        // GET: /earchive/titlesearch/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /earchive/titlesearch/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /earchive/titlesearch/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /earchive/titlesearch/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /earchive/titlesearch/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /earchive/titlesearch/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Title Search Actions

        public ActionResult searchbyLocation(int? page, String town = "", String lga = "", String street = "")
        {
            var result = LandTitles.search_for_property(town, lga, street);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }
        #endregion

    }
}
