using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

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
        public string location = "";

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
                    return dt;
                }
                catch (SqlException)
                {
                    return dt;
                    throw;                    
                }
                finally
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }

        }

        public List<Car> readAll()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            List<Car> cars = new List<Car>();

            using (SqlCommand cmd = new SqlCommand(readSQL, getConnection.getConnection()))
            {
                SqlDataReader dr = null;
                try
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cars.Add(new Car(Int32.Parse(dr["CarID"].ToString()), dr["Plate"].ToString(), dr["Manufacturer"].ToString(), dr["Model"].ToString(), Double.Parse(dr["PricePerDay"].ToString()), dr["Location"].ToString()));
                    }
                    return cars;
                }
                catch (SqlException)
                {
                    return cars;
                    throw;
                }
                finally
                {
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }

        }

        public int confirmID(string column, string paramValue)
        {
            int no = 0;
            string getID;
            if (column == "CarID" || column == "Location")
            {
                getID = "SELECT " + column + " FROM " + table_Name + " WHERE Plate = @param;";
            }
            else
            {
                getID = "SELECT " + column + " FROM " + table_Name + " WHERE Model = @param;";
            }

            using (SqlCommand cmd = new SqlCommand(getID, getConnection.getConnection()))
            {
                SqlDataReader dr = null;
                try
                {
                    cmd.Parameters.AddWithValue("@param", paramValue);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (column == "Location")
                        {
                            no = 1;
                            location = dr["Location"].ToString();
                        }
                        else
                        {
                            if (column == "Model")
                            {
                                no = 1;
                            }
                            else
                            {
                                no = dr.GetInt32(no);
                            }
                        }
                    }
                    return no;
                }
                catch (SqlException)
                {
                    return no;
                    throw;                   
                }
                finally
                {
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }

        }

        public int confirmOverallLocation(string column, string paramValue)
        {
            int no = 0;
            string getID;
            getID = "SELECT " + column + " FROM " + table_Name + " WHERE Location = @param;";

            using (SqlCommand cmd = new SqlCommand(getID, getConnection.getConnection()))
            {
                SqlDataReader dr = null;
                try
                {
                    cmd.Parameters.AddWithValue("@param", paramValue);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        no = 1;
                    }
                    return no;
                }
                catch (SqlException)
                {
                    return no;
                    throw;                   
                }
                finally
                {
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }

        }

        public List<Car> searchCars(string plate, string model, string city, DateTime presentStartDate, DateTime presentEndDate)
        {
            string searchSQL;
            int counter = 0;
            List<Car> cars = new List<Car>();

            if (plate != "" || model != "" || city != "")
            {
                searchSQL = "select DISTINCT c.CarID, c.Plate, c.Manufacturer, c.Model, c.PricePerDay, c.Location from Cars c" +
                            " left join Reservations r on c.CarID = r.CarID where r.CarID is null" +
                            " and (c.Plate = @plate or c.Model = @model or c.Location = @location)" +
                            " union" +
                            " select DISTINCT c.CarID, c.Plate, c.Manufacturer, c.Model, c.PricePerDay, c.Location from Cars c" +
                            " inner join Reservations r on c.CarID = r.CarID" +
                            " where (c.Plate = @plate or c.Model = @model or c.Location = @location)";
            }
            else
            {
                searchSQL = "select DISTINCT c.CarID, c.Plate, c.Manufacturer, c.Model, c.PricePerDay, c.Location from Cars c" +
                            " left join Reservations r on c.CarID = r.CarID where r.CarID is null " +
                            " union" +
                            " select DISTINCT c.CarID, c.Plate, c.Manufacturer, c.Model, c.PricePerDay, c.Location from Cars c" +
                            " inner join Reservations r on c.CarID = r.CarID";
            }

            using (SqlCommand cmd = new SqlCommand(searchSQL, getConnection.getConnection()))
            {
                SqlDataReader dr = null;
                try
                {
                    cmd.Parameters.AddWithValue("@plate", plate);
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@location", city);
                    cmd.Parameters.AddWithValue("@presentStartDate", presentStartDate);
                    cmd.Parameters.AddWithValue("@presentEndDate", presentEndDate);
                    dr = cmd.ExecuteReader();

                    var message = "";
                    while (dr.Read())
                    {
                        Car car = new Car();
                        car.carID = Int32.Parse(dr["CarID"].ToString());
                        car.plate = dr["Plate"].ToString();
                        car.manufacturer = dr["Manufacturer"].ToString();
                        car.model = dr["Model"].ToString();
                        car.price = Double.Parse(dr["PricePerDay"].ToString());
                        car.location = dr["Location"].ToString();

                        cars.Add(car);
                        counter++;
                    }
                    message = string.Join(Environment.NewLine, cars);

                    if (counter == 1)
                    {
                        //MessageBox.Show("Car successfully found:" + message);
                        return cars;
                    }
                    else if (counter > 1)
                    {
                        //MessageBox.Show(cars.Count() + " Cars found:\n\n" + message);
                        return cars;
                    }
                    else
                    {
                        //MessageBox.Show("No cars found for the specified criterias");
                    }                   
                    return null;
                }
                catch (SqlException)
                {
                    return null;
                    throw;                  
                }
                finally
                {
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }

        }
    }
}