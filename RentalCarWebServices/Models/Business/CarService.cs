﻿using RentalCarWebServices.Models.Database.DAO;
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
            try
            {
                return carDAO.readAllInDataTable();
            }
            catch (SqlException ex)
            {
                return null;
                throw new Exception("Error finding data: " + ex.Message);               
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
            catch (SqlException ex)
            {
                return cars;
                throw new Exception("Error finding data: " + ex.Message);
            }
        }

        public int confirmID(string column, string paramValue)
        {
            try
            {
                return carDAO.confirmID(column, paramValue);
            }
            catch (SqlException ex)
            {
                return 0;
                throw new Exception("Error getting car ID: " + ex.Message);                
            }
        }

        public int confirmOverallLocation(string column, string paramValue)
        {
            try
            {
                return carDAO.confirmOverallLocation(column, paramValue);
            }
            catch (SqlException ex)
            {
                return 0;
                throw new Exception("Error getting location: " + ex.Message);                
            }
        }
    }
}