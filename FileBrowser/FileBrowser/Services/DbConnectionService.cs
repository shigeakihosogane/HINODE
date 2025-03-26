using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Services
{
    public class DbConnectionService
    {
        private String _connectionString;

        public DbConnectionService()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"hinode-server.database.windows.net";
            builder.InitialCatalog = "HINODEDB";
            builder.UserID = "hinode";
            builder.Password = "Hkanri8739";
            builder.TrustServerCertificate = true;
            _connectionString = builder.ToString();
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
