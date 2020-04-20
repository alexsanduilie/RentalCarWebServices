using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RentalCarWebServices.Models.Database
{
    public class GetConnection
    {
        public SqlConnection getConnection()
        {
            InitializeDb initializeDb = new InitializeDb();
            return initializeDb.getConnection();
        }
    }
}