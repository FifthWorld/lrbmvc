using ApplicationLibrary;
using LRB;
using LRB.Lib;
using LRB.Lib.Domain;
using LRBMvc.Models;
using SimpleSecurity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LRBMvc.Controllers
{
    [Authorize]
    public class LandApplicationController : Controller
    {
        LandBureau Bureau;

        public LandApplicationController()
            : base()
        {
            Bureau = new LandBureau(this);

        }
        //
        // GET: /LandApplication
        public ActionResult Index()
        {
            var username = User.Identity.Name;

            return View(LandRecords.GetApplications(WebSecurity.CurrentUserName));
        }

        public ActionResult Create()
        {
            Requirement model = new Requirement();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Requirement model)
        {
            var app = LandRecords.CreateApplication(model);
            Bureau.SetAppId(app.Id);
            Bureau.setRequirement(model);
            return RedirectToAction("ContactInformation");
        }

        public ActionResult ContactInformation()
        {
            var appId = Session["appId"];

            if (null == appId)
            {
                return RedirectToAction("Index");
            }
            var app = LandRecords.GetApplication(int.Parse(appId.ToString()));
            if (null == app)
            {
                return RedirectToAction("Index");
            }
            var appType = Bureau.GetRequirement().applicationType;
            ViewBag.ApplicationType = appType;
            Party model = app.ContactPerson;
            if (model == null)
            {
                model = new Party();
            }
            return View(model);
        }

        public ActionResult Continue(int id)
        {
            var app = LandRecords.GetApplication(id);
            Bureau.SetAppId(id);
            var requirement = new Requirement()
            {
                applicationType = app.ApplicationType,
                landSize = Convert.ToInt32(app.PrimaryProperty.LandSize),
                landUse = app.PrimaryProperty.LandUse,
                landSizeUnit = app.PrimaryProperty.LandSizeUnit
            };
            Bureau.setRequirement(requirement);
            return RedirectToAction("ContactInformation");
        }

        [HttpPost]
        public ActionResult ContactInformation(Party model)
        {
            var appId = Session["appId"];
            if (appId == null)
            {
                //Flash.Instance.Notice("Your Session Has Expired, Please select incomplete application");
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                LandRecords.SaveApplication(int.Parse(appId.ToString()), model);
            }

            return RedirectToAction("PropertyInformation");
        }

        public ActionResult PropertyInformation()
        {
            var appId = Session["appId"];

            if (null == appId)
            {
                return RedirectToAction("Index");
            }
            var app = LandRecords.GetApplication(int.Parse(appId.ToString()));
            if (null == app)
            {
                return RedirectToAction("Index");
            }
            List<int> months = new List<int>();
            List<int> years = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                years.Add(i);
            }
            for (int i = 1; i < 13; i++)
            {
                months.Add(i);
            }
            ViewBag.Months = months;
            ViewBag.Years = years;
            Property model = app.Properties.FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult PropertyInformation(Property model)
        {
            var appId = Session["appId"];
            if (appId == null)
            {
                //Flash.Instance.Notice("Your Session Has Expired, Please select incomplete application");
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                LandRecords.SaveApplication(int.Parse(appId.ToString()), model);
            }

            return RedirectToAction("OptionalInformation");
        }

        public ActionResult OptionalInformation()
        {
            var appId = Session["appId"];

            if (null == appId)
            {
                return RedirectToAction("Index");
            }
            var app = LandRecords.GetApplication(int.Parse(appId.ToString()));
            if (null == app)
            {
                return RedirectToAction("Index");
            }

            Optional model = app.OptionalRequirement;
            if (model == null)
            {
                model = new Optional();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult OptionalInformation(Optional model)
        {
            var appId = Session["appId"];
            if (appId == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                LandRecords.SaveApplication(int.Parse(appId.ToString()), model);
            }

            return RedirectToAction("RequiredDocuments");
        }

        public ActionResult RequiredDocuments()
        {
            var appId = Session["appId"];

            if (null == appId)
            {
                return RedirectToAction("Index");
            }
            var app = LandRecords.GetApplication(int.Parse(appId.ToString()));
            var requirement = Bureau.GetRequirement();
            ViewBag.Requirement = requirement;
            if (null == app)
            {
                return RedirectToAction("Index");
            }

            DocumentManager model = LandRecords.GetDocumentManager(Bureau.GetAppId().Value);
            if (model == null)
            {
                model = new DocumentManager();
            }
            ViewBag.Documents = app.Documents;
            ViewBag.MissingDocuments = Bureau.GetRemainingDocuments(app.Documents);
            ViewBag.IsComplete = Bureau.IsComplete(app.Documents);
            return View(model);
        }

        [HttpPost]
        public ActionResult RequiredDocuments(DocumentManager model,
            IEnumerable<HttpPostedFileBase> files,
            HttpPostedFileBase evidence,
            HttpPostedFileBase developmentlevy,
            HttpPostedFileBase fireservice,
            HttpPostedFileBase policereport,
            HttpPostedFileBase certificate,
            HttpPostedFileBase feasibilityreport,
            HttpPostedFileBase powers,
            HttpPostedFileBase passport,
            HttpPostedFileBase surveyplan)
        {
            var appId = Session["appId"];

            if (null == appId)
            {
                return RedirectToAction("Index");
            }
            Bureau.SaveDocument(evidence, Bureau.EVIDENCE, "standardDocument");
            Bureau.SaveDocument(developmentlevy, Bureau.DEVELOPMENT_LEVY, "standardDocument");
            Bureau.SaveDocument(certificate, Bureau.CERTIFICATE, "standardDocument");
            Bureau.SaveDocument(surveyplan, Bureau.SURVEY_PLAN, "cadastralSurvey");
            Bureau.SaveDocument(fireservice, Bureau.FIRE_SERVICE, "standardDocument");
            Bureau.SaveDocument(policereport, Bureau.POLICE_REPORT, "standardDocument");
            Bureau.SaveDocument(feasibilityreport, Bureau.FEASIBILITY, "standardDocument");
            Bureau.SaveDocument(powers, Bureau.POWERS, "powerOfAttorney");
            Bureau.SaveDocument(passport, Bureau.PASSPORT, "idVerification");

            LandRecords.SaveApplication(int.Parse(appId.ToString()), model);
            return RedirectToAction("RequiredDocuments");
        }

        public ActionResult Summary()
        {
            var appId = Session["appId"];
            if (null == appId)
            {
                return RedirectToAction("Index");
            }
            var model = LandRecords.GetApplication(int.Parse(appId.ToString()));
            var c = model.ContactPerson;
            var p = model.PrimaryProperty;
            if (null == model)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Complete()
        {
            var appId = Session["appId"];
            if (null == appId)
            {
                return RedirectToAction("Index");
            }
            var model = LandRecords.GetApplication(int.Parse(appId.ToString()));
            if (null == model)
            {
                //Flash.Instance.Error("Application with that Id does not exist");
                return RedirectToAction("Index");
            }
            SolaService solaService = new SolaService(int.Parse(appId.ToString()));
            var solaApp = solaService.SubmitToSola();
            LandRecords.UpdateStatus(Bureau.GetAppId().Value, solaApp.statusCode, solaApp.id, solaApp.nr);
            int Id = Convert.ToInt32(appId);
            LandRecords.SubmitApplication(Id);
            Session["appId"] = null;
            return RedirectToAction("View", "LandApplication", new { id = Id });
        }

        public ActionResult View(int id)
        {

            //if (Bureau.isSolaOnline() == true)
            //{
            //    Bureau.UpdateApplicationStatus(id);
            //}
            var app = LandRecords.GetApplication(id);            
            var fees = LandFees.Calculate_Consent_Fees(
                2013, 
                (float)app.PrimaryProperty.LandSize, 
                LandFees.getLandValue(app),
                LandFees.getLandDevelopment(app), 
                LandFees.getLandUse(app)
                );
            ViewBag.PropertyValue = fees;
            ViewBag.LandUse = LandFees.getLandUse(app).ToString();
            return View(app);
        }


        //
        // GET: /LandApplications/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            var app = LandRecords.GetApplication(id);
            if (app == null)
            {
                return HttpNotFound();
            }
            return View(app);
        }

        //
        // POST: /LandApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandRecords.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
