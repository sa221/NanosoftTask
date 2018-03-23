using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using NanoSoftExam.Models;
using NanoSoftExam.Data;

namespace NanoSoftExam.Controllers
{
    public class ClassController : Controller
    {
        private StudentContext db = new StudentContext();

        // GET: /Class/
        public ActionResult Index()
        {
            IEnumerable<Class> clsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Class").Result;
            clsList = response.Content.ReadAsAsync<IEnumerable<Class>>().Result;
            return View(clsList);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Class());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Class/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Class>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(Class cls)
        {
            if (cls.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Class", cls).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Class/" + cls.Id, cls).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Class/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}

