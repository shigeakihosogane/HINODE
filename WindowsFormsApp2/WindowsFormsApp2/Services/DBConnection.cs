using System;
using System.Data.SqlClient;

namespace WindowsFormsApp2.Services
{
    public static class DBConnection
    {
        private static String _connectionString;

        static DBConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"hinode-server.database.windows.net";
            builder.InitialCatalog = "HINODEDB";
            builder.UserID = "hinode";
            builder.Password = "Hkanri8739";
            builder.TrustServerCertificate = true;
            _connectionString = builder.ToString();
        }

        public static string GetConnectionString()
        {
            return _connectionString; 
        }




    }
}
