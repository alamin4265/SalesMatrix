using SalesMatrix.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SalesMatrix.Controllers
{
    public class ResourceController : Controller
    {
        private string Base_URL = "http://localhost:1848/api/";

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Resource resource)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                HttpResponseMessage response = client.PostAsJsonAsync("resource", resource).Result;
                response.EnsureSuccessStatusCode();
                return Json("Resource created", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Failed", JsonRequestBehavior.AllowGet);
            }
        }
    }
}