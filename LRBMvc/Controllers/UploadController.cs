using LRB.Lib;
using LRB.Lib.Domain;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LRBMvc.Controllers
{
    public class UploadController : ApiController
    {
        //public async Task<HttpResponseMessage> PostFormData()
        //{
        //    // Check if the request contains multipart/form-data.
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    string root = HttpContext.Current.Server.MapPath("~/App_Data");
        //    var provider = new MultipartFormDataStreamProvider(root);
        //    var streamProvider = new MultipartMemoryStreamProvider();
        //    Document doc = new Document();
        //    try
        //    {
        //        // Read the form data.
        //        var result = await Request.Content.ReadAsMultipartAsync(provider);
        //        var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t => 
        //        {
        //            doc.FileName = provider.FormData["appId"];
        //            doc.Content = streamProvider.Contents[0].ReadAsByteArrayAsync().Result;
        //        });
                
                
        //        LandRecords.SaveApplication(appId, doc);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (System.Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}

    }
}
