using ApplicationLibrary;
using LRB;
using LRB.Lib;
using LRB.Lib.Domain;
using LRBMvc.Models;
using SimpleSecurity;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace LRBMvc.Controllers
{
    [Authorize]
    public class LandApplicationController : Controller
    {
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
            Session["appId"] = app.Id;
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

            Party model = app.ContactPerson;
            if (model ==null)
            {
                model = new Party();
            }
            return View(model);
        }

        public ActionResult Continue(int id)
        {
            Session["appId"] = id;
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

            return RedirectToAction("RequiredDocuments");
        }

        public ActionResult RequiredDocuments()
        {
            var appId = Session["appId"];
            if (null == appId)
            {
                //Flash.Instance.Notice("Your Session Has Expired, Please select incomplete application");
                return RedirectToAction("Index");
            }
            var model = LandRecords.GetApplication(int.Parse(appId.ToString()));
            if (null == model)
            {
                //Flash.Instance.Error("Application with that Id does not exist");
                return RedirectToAction("Index");
            }
            return View(model.Documents);
        }

        public ActionResult Summary()
        {
            var appId = Session["appId"];
            if (null == appId)
            {
                //Flash.Instance.Notice("Your Session Has Expired, Please select incomplete application");
                return RedirectToAction("Index");
            }
            var model = LandRecords.GetApplication(int.Parse(appId.ToString()));
            var c = model.ContactPerson;
            var p = model.PrimaryProperty;
            if (null == model)
            {
                //Flash.Instance.Error("Application with that Id does not exist");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Complete()
        {
            var appId = Session["appId"];
            if (null == appId)
            {
                //Flash.Instance.Notice("Your Session Has Expired, Please select incomplete application");
                return RedirectToAction("Index");
            }
            var model = LandRecords.GetApplication(int.Parse(appId.ToString()));
            if (null == model)
            {
                //Flash.Instance.Error("Application with that Id does not exist");
                return RedirectToAction("Index");
            }
            LandRecords.SubmitApplication(int.Parse(appId.ToString()));
            SolaService solaService = new SolaService(int.Parse(appId.ToString()));
            var solaApp = solaService.SubmitToSola();
            return RedirectToAction("Index");
        }

        public ActionResult View(int id)
        {
            var app = LandRecords.GetApplication(id);
            //Flash.Instance.Notice("Something happens");
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
