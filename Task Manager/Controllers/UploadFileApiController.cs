using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    public class UploadFileApiController : System.Web.Http.ApiController
    {
        TaskContext db = new TaskContext();
        [HttpPost]
        public void Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var sessionId = HttpContext.Current.Session;
                int name = 0;
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    if (sessionId["project"] != null)
                    {
                        var filePath = db.files.OrderByDescending(p => p.id).FirstOrDefault().fileName;
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                        break;
                    }
                    else
                    {
                        name = db.product.OrderByDescending(p => p.id).FirstOrDefault().id;
                        var filePath = HttpContext.Current.Server.MapPath("~/Images/" + name + ".jpg");
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                        break;
                    }
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }



        [HttpPost]
        public String StatusDocument(int id)
        {
            var Session = HttpContext.Current.Session;
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var statsdocument = db.ticketdocuments.OrderByDescending(p => p.id).FirstOrDefault().documentPath;
                    postedFile.SaveAs(statsdocument);
                    docfiles.Add(statsdocument);
                    break;
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                return ("File Added Sucessfully");
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
                return (result.ToString());
            }
        }
    }
}