using RentalCarWebServices.Models.Database.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace RentalCarWebServices.Models.Business
{
    public class CarValidationsService
    {
        private static readonly CarValidationsService instance = new CarValidationsService();
        static CarValidationsService()
        {
        }
        private CarValidationsService()
        {
        }
        public static CarValidationsService Instance
        {
            get
            {
                return instance;
            }
        }

        private static CarDAO carDAO = CarDAO.Instance;
        private static CarService carService = CarService.Instance;

        public List<Car> searchCars(string plate, string model, string city, DateTime presentStartDate, DateTime presentEndDate)
        {
            List<Car> cars = new List<Car>();
            try
            {
                cars = carDAO.searchCars(plate, model, city, presentStartDate, presentEndDate);
                return cars;
            }
            catch (SqlException)
            {
                return cars;
                throw;               
            }
        }

        public bool validateCarPlate(string carPlate)
        {
            bool plate = true;
            if (!String.IsNullOrEmpty(carPlate))
            {
                int carP = carService.confirmID("CarID", carPlate);
                if (carP == 0)
                {
                    if (!Regex.IsMatch(carPlate, "[A-Z]{2} [0-9]{2} [A-Z]{3}"))
                    {
                        plate = false;
                    }
                    else
                    {
                        plate = false;
                    }
                }
            }
            return plate;
        }

        public bool validateCarModel(string carModel)
        {
            bool cl = true;
            if (!String.IsNullOrEmpty(carModel))
            {
                int model = carService.confirmID("Model", carModel);
                if (model == 0)
                {
                    cl = false;
                }
            }
            return cl;
        }

        public bool validateCity(string city)
        {
            bool cl = true;
            if (!String.IsNullOrEmpty(city))
            {
                int location = carService.confirmOverallLocation("Location", city);
                if (location == 0)
                {
                    cl = false;
                }
            }
            return cl;
        }
    }
}