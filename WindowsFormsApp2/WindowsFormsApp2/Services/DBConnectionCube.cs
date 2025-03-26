using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Services
{
    public static class DBConnectionCube
    {
        private static String _connectionString;

        static DBConnectionCube()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"SV201710";
            builder.InitialCatalog = "HINODEDB";
            builder.UserID = "sa";
            builder.Password = "cube%6614";
            builder.TrustServerCertificate = true;
            _connectionString = builder.ToString();
        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}


