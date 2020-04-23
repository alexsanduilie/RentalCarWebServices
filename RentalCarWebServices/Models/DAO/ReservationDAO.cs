using RentalCarWebServices.Models.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RentalCarWebServices.Models.DAO
{
    public class ReservationDAO
    {
        private static readonly ReservationDAO instance = new ReservationDAO();

        static ReservationDAO()
        {
        }

        private ReservationDAO()
        {
        }

        public static ReservationDAO Instance
        {
            get
            {
                return instance;
            }
        }

        GetConnection getConnection = new GetConnection();
        private static string table_Name = "Reservations";

        public DataTable readByPlate(string plate)
        {
            string readSQL = "SELECT * FROM Reservations WHERE CarPlate = @plate;";
            SqlDataAdapter dataAdapter;
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(readSQL, getConnection.getConnection()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@plate", plate);
                    dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dt);

                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return dt;
                }
                catch (SqlException ex)
                {
                    return dt;
                    throw new Exception("SQL error: " + ex.Message);                  
                }
            }

        }
    }
}