using RentalCarWeb.ListCarsServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalCarWeb.Controllers
{
    public class HomeController : Controller
    {
        IEnumerable<Car> allCars;
        ListCarsServiceReference.ListCarsServiceSoapClient listCarsServiceSoap = new ListCarsServiceReference.ListCarsServiceSoapClient();
        public ActionResult Menu()
        {
            try
            {
                listCarsServiceSoap.Open();
                allCars = listCarsServiceSoap.readAll();
                ViewBag.Message = "Welcome to RentC, your brand new solution to manage and control your company's data without missing anything!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }            
            return View();
        }
    }
}