using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Task_Manager.Controllers
{
    public class DashboardApiController : ApiController
    {
        [ HttpPost]
        public int View(string id)
        {
            var session = HttpContext.Current.Session;
            session["dashboard"] = id;
            return 0;          
        }
    }
}
