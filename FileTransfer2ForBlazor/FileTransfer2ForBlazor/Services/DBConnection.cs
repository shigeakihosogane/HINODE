using Microsoft.Data.SqlClient;

namespace FileTransfer2ForBlazor.Services
{
    public class DBConnection
    {
        private String _connectionString;

        public DBConnection()
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

        public async Task<bool> CheckDatabaseConnection()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                return true; // 接続成功
            }
            catch (Exception ex)
            {
                Console.WriteLine($"接続エラー: {ex.Message}");
                return false; // 接続失敗
            }
        }



    }
}
