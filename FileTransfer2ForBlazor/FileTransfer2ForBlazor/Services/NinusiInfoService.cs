using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Services
{
    public class NinusiInfoService
    {
        private readonly string _connectionString;
        public NinusiInfoService(DBConnection connection)
        {
            _connectionString = connection.GetConnectionString();
        }

        public async Task UpdateNinusimeiAsync(int ninusiID, string ninusimei)
        {
            var sql = @"UPDATE T_TF_M_NinusiInfo 
SET     名称_TF = @NNAME 
WHERE   荷主ID = @NID
";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            try
            {
                await connection.OpenAsync();
                command.Parameters.Add(new SqlParameter("@NID", ninusiID));
                command.Parameters.Add(new SqlParameter("@NNAME", ninusimei));
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQLエラー: " + ex.Message);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("UpdateNinusimeiAsync: " + ex.Message);
            }

        }
        public async Task UpdateFaxNumberAsync(int ninusiID, string faxnumber)
        {
            var sql = @"UPDATE T_TF_M_NinusiInfo 
SET     FAX番号 = @FNUMBER 
WHERE   荷主ID = @NID
";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            try
            {
                await connection.OpenAsync();
                command.Parameters.Add(new SqlParameter("@NID", ninusiID));
                command.Parameters.Add(new SqlParameter("@FNUMBER", faxnumber));
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQLエラー: " + ex.Message);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("UpdateFaxNumberAsync: " + ex.Message);
            }

        }








    }
}
