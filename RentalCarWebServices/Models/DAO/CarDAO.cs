using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RentalCarWebServices.Models.Database.DAO
{
    public class CarDAO
    {
        private static readonly CarDAO instance = new CarDAO();
        static CarDAO()
        {
        }

        private CarDAO()
        {
        }

        public static CarDAO Instance
        {
            get
            {
                return instance;
            }
        }

        GetConnection getConnection = new GetConnection();
        private static string table_Name = "Cars";

        public DataTable readAllInDataTable()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            SqlDataAdapter dataAdapter;
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(readSQL, getConnection.getConnection()))
            {
                try
                {
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

        public List<Car> readAll()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            List<Car> cars = new List<Car>();

            using (SqlCommand cmd = new SqlCommand(readSQL, getConnection.getConnection()))
            {
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        cars.Add(new Car(Int32.Parse(dr["CarID"].ToString()), dr["Plate"].ToString(), dr["Manufacturer"].ToString(), dr["Model"].ToString(), Double.Parse(dr["PricePerDay"].ToString()), dr["Location"].ToString()));
                    }

                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return cars;
                }
                catch (SqlException ex)
                {
                    return cars;
                    throw new Exception("SQL error: " + ex.Message);
                }
            }

        }
    }
}