using FileBrowser.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Services
{
    public class CustomerService
    {

        private readonly string _connectionString;

        public CustomerService(DbConnectionService dbConnectionService)
        {
            _connectionString = dbConnectionService.GetConnectionString();
        }

        public async Task<List<string>> GetCustomersAsync()
        {
            var customers = new List<string>();
            var sql = @"SELECT ConsignorName FROM dbo.T_TF_D_FileRegistry WHERE (ConsignorName <> '') OR (ConsignorName IS NOT NULL) GROUP BY ConsignorName";


            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            try
            {
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    customers.Add(Convert.ToString(reader["ConsignorName"]) ?? "");                    
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }
            return customers;
        }









    }
}
