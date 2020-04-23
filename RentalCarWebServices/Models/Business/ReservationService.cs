using RentalCarWebServices.Models.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RentalCarWebServices.Models.Business
{
    public class ReservationService
    {
        private static readonly ReservationService instance = new ReservationService();
        private ReservationDAO reservationDAO;
        static ReservationService()
        {
        }
        private ReservationService()
        {
            reservationDAO = ReservationDAO.Instance;
        }
        public static ReservationService Instance
        {
            get
            {
                return instance;
            }
        }

        public DataTable readByPlate(string plate)
        {
            try
            {
                return reservationDAO.readByPlate(plate);
            }
            catch (SqlException ex)
            {
                return null;
                throw new Exception("Error finding data: " + ex.Message);               
            }
        }
    }
}