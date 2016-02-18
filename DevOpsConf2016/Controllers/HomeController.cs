﻿using System.Web.Mvc;

namespace DevOpsConf2016.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Sessions()
        {
            ViewBag.Message = "Sessions";

            return View();
        }

        public ActionResult ConferenceChairs()
        {
            ViewBag.Message = "Conference Chairs";

            return View();
        }
    }
}