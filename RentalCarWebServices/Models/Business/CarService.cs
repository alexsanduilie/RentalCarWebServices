using RentalCarWebServices.Models.Database.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RentalCarWebServices.Models.Business
{
    public class CarService
    {
        private static readonly CarService instance = new CarService();
        private CarDAO carDAO;
        static CarService()
        {
        }
        private CarService()
        {
            carDAO = CarDAO.Instance;
        }
        public static CarService Instance
        {
            get
            {
                return instance;
            }
        }

        public DataTable readAllInDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = carDAO.readAllInDataTable();
                return dt;
            }
            catch (SqlException)
            {
                return dt;
                throw;               
            }
        }

        public List<Car> readAll()
        {
            List<Car> cars = new List<Car>();
            try
            {
                cars = carDAO.readAll();
                return cars;
            }
            catch (SqlException)
            {
                return cars;
                throw;
            }
        }

        public int confirmID(string column, string paramValue)
        {
            try
            {
                return carDAO.confirmID(column, paramValue);
            }
            catch (SqlException)
            {
                return 0;
                throw;                
            }
        }

        public int confirmOverallLocation(string column, string paramValue)
        {
            try
            {
                return carDAO.confirmOverallLocation(column, paramValue);
            }
            catch (SqlException)
            {
                return 0;
                throw;                
            }
        }
    }
}