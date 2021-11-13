using BHMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace BHMS.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult LoggedIn()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult SignUp()
        {

            return View();
        }

        
    }
}