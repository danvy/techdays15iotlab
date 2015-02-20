using IoTLab.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTLab.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        public ActionResult Switch()
        {
            DeviceModel.Instance.LightOn = !DeviceModel.Instance.LightOn;
            return RedirectToAction("Index");
        }
    }
}
