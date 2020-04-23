using RentalCarWebServices.Models.Business;
using RentalCarWebServices.Models.Database.DAO;
using RentalCarWebServices.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

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
        CarValidationsService carValidations = CarValidationsService.Instance;
        ReservationValidationsService reservationValidations = ReservationValidationsService.Instance;

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

        [WebMethod]
        public List<Car> searchCars(string plate, string model, string city, DateTime presentStartDate, DateTime presentEndDate)
        {
            List<Car> cars = new List<Car>();
            cars = carValidations.searchCars(plate, model, city, presentStartDate, presentEndDate);
            return cars;
        }

        [WebMethod]
        public bool validateCarPlate(string carPlate, string message)
        {
            return carValidations.validateCarPlate(carPlate, message);
        }

        [WebMethod]
        public bool validateCarModel(string carModel, string message)
        {
            return carValidations.validateCarModel(carModel, message);
        }

        [WebMethod]
        public bool validateCity(string city, string message)
        {
            return carValidations.validateCity(city, message);
        }

        [WebMethod]
        public bool validateDate(DateTime startDate, DateTime endDate, string message)
        {
            return reservationValidations.validateDate(startDate, endDate, message);
        }

        [WebMethod]
        public bool validateRentPeriod(string plate, string message, DateTime presentStartDate, DateTime presentEndDate, string condition, Reservation reservation)
        {
            return reservationValidations.validateRentPeriod(plate, message, presentStartDate, presentEndDate, condition, reservation);
        }
    }
}
