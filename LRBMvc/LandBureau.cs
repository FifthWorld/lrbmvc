using LRB.Lib;
using LRB.Lib.Domain;
using org.sola.services.boundary.wsclients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRBMvc
{
    public class LandBureau
    {
        Controller ctrl;
        public LandBureau(Controller Ctrl)
        {
            ctrl = Ctrl;
        }
        public int? GetAppId()
        {
            var appId = ctrl.Session["appId"];
            if (appId == null)
            {
                return null;
            }
            return int.Parse(appId.ToString());
        }
        public void SetAppId(int appId)
        {
            ctrl.Session["appId"] = appId;
        }
        public Requirement GetRequirement()
        {
            var req = ctrl.Session["Requirement"];
            if (req == null)
            {
                return null;
            }
            return req as Requirement;
        }
        public void setRequirement(Requirement req)
        {
            ctrl.Session["Requirement"] = req;
        }

        public void SaveDocument(HttpPostedFileBase file, string filetype, string documentType)
        {
            if (file != null)
            {
                Document doc = new Document();
                doc.FileName = file.FileName;
                doc.DocumentType = filetype;
                doc.Extension = file.ContentType;
                doc.Description = "pdf";
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    doc.Content = br.ReadBytes(int.Parse(file.InputStream.Length.ToString()));
                }
                LandRecords.SaveDocument(GetAppId().Value, doc);
            }
        }

        public string IsComplete(IEnumerable<Document> documents)
        {
            var response = "";
            var r = GetRequirement();
            IEnumerable<String> documentTypes = from doc in documents select doc.DocumentType;
            if (!documentTypes.Contains(SURVEY_PLAN) || !documentTypes.Contains(DEVELOPMENT_LEVY) || !documentTypes.Contains(EVIDENCE))
            {
                response = "disabled";
            }
            if (r.applicationType == "Corporate" && !documentTypes.Contains(CERTIFICATE))
            {
                response = "disabled";
            }
            if (r.landSize > 5000 && !documentTypes.Contains(FEASIBILITY))
            {
                response = "disabled";
            }
            return response;
        }

        public IEnumerable<String> GetRemainingDocuments(IEnumerable<Document> documents)
        {
            List<String> docs = new List<string>();
            var response = "";
            var r = GetRequirement();
            IEnumerable<String> documentTypes = from doc in documents select doc.DocumentType;
            if (!documentTypes.Contains(SURVEY_PLAN))
            {
                docs.Add(SURVEY_PLAN);
            }
            if (!documentTypes.Contains(DEVELOPMENT_LEVY))
            {
                docs.Add(DEVELOPMENT_LEVY);
            }
            if (!documentTypes.Contains(EVIDENCE))
            {
                docs.Add(EVIDENCE);
            }
            if (r.applicationType == "Corporate" && !documentTypes.Contains(CERTIFICATE))
            {
                docs.Add(CERTIFICATE);
            }
            if (r.landSize > 5000 && !documentTypes.Contains(FEASIBILITY))
            {
                docs.Add(FEASIBILITY);
            }
            return docs as IEnumerable<string>;
        }

        public bool isSolaOnline()
        {
            ICaseManagementService caseMgmtSvc = CasemanagementProxy.Instance;
            caseMgmtSvc.SetCredentials("test", "test");
            return caseMgmtSvc.CheckConnection();
        }

        public void UpdateApplicationStatus(int Id)
        {
            var app = LandRecords.GetApplication(Id);
            ICaseManagementService caseMgmtSvc = CasemanagementProxy.Instance;
            caseMgmtSvc.SetCredentials("test", "test");
            var appTO = caseMgmtSvc.GetApplication(app.SolaId);
            LandRecords.UpdateStatus(app.Id, appTO.statusCode);
        }

        public string getExtension(string contentType)
        {
            switch (contentType)
            {
                case "text/plain":
                    return "txt";
                case "application/pdf":
                    return "pdf";
                case "image/jpeg":
                    return "jpeg";
                case "image/png":
                    return "png";
                case "application/msword":
                    return "doc";
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return "docx";
                case "application/vnd.ms-excel":
                    return "xls";
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    return "xlsx";
                default:
                    return "txt";
            }
        }


        public string EVIDENCE = "Evidence of Ownership";
        public string FIRE_SERVICE = "Fire Service Report";
        public string FEASIBILITY = "Feasibility Report";
        public string DEVELOPMENT_LEVY = "Development Levy";
        public string SURVEY_PLAN = "Survey Plan";
        public string CERTIFICATE = "Certificate of Incorporation";
        public string POWERS = "Registration Particulars of Powers of Attorney";
        public string POLICE_REPORT = "Police Report";
        public string PASSPORT = "Scanned photographic passport";
    }

    public static class LandMessaging
    {
        public static string Message { get; set; }
    }

}