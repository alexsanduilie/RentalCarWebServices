using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalCarWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Menu()
        {
            ViewBag.Message = "Welcome to RentC, your brand new solution to manage and control your company's data without missing anything!";
            return View();
        }
    }
}