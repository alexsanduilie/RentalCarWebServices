using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RentalCarWebServices.Models.Database
{
    public class InitializeDb : IDisposable
    {
        private SqlConnection cnn;

        public InitializeDb()
        {
            if ((cnn = con()) == null)
            {
                this.Dispose();
            }

        }

        public SqlConnection getConnection()
        {
            return cnn;
        }

        private SqlConnection con()
        {
            SqlConnection conn = null;
            string server = "DESKTOP-VBF6SCE\\SQLEXPRESS";
            string db = "academy_net";
            string connString;

            connString = string.Format("Server={0};Database={1};Trusted_Connection=SSPI;", server, db);
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = connString;
                conn.Open();
                return conn;
            }
            catch
            {
                conn.Dispose();
                return null;
            }
        }
        public void Dispose()
        {
            if (cnn != null)
            {
                cnn.Dispose();
            }
        }
    }
}