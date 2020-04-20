using RentalCarWebServices.Models.Business;
using RentalCarWebServices.Models.Database.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RentalCarWebServices
{
    /// <summary>
    /// Summary description for ListCarsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ListCarsService : System.Web.Services.WebService
    {
        CarService carService = CarService.Instance;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataTable readAllInDataTable()
        {
            DataTable dt = new DataTable();
            dt = carService.readAllInDataTable();
            dt.TableName = "Cars";
            return dt;
        }

        [WebMethod]
        public List<Car> readAll()
        {
            List<Car> cars = new List<Car>();
            cars = carService.readAll();
            return cars;
        }
    }
}
