using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Services
{
    public static class NinusiInfoService
    {
        private static String _connectionString;
        static NinusiInfoService()
        {
            _connectionString = DBConnection.GetConnectionString();
        }
        public async static Task UpdateNinusimeiAsync(int ninusiID, string ninusimei)
        {
            var sql = @"UPDATE T_TF_M_NinusiInfo 
SET     名称_TF = @NNAME 
WHERE   荷主ID = @NID
";
            await Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    { 
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("@NID", ninusiID));
                        command.Parameters.Add(new SqlParameter("@NNAME", ninusimei));
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("SQLエラー: " + ex.Message);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("データベース接続エラー: " + ex.Message);
                    }
                }
            });
        }
        public async static Task UpdateFaxNumberAsync(int ninusiID, string faxnumber)
        {
            var sql = @"UPDATE T_TF_M_NinusiInfo 
SET     FAX番号 = @FNUMBER 
WHERE   荷主ID = @NID
";
            await Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("@NID", ninusiID));
                        command.Parameters.Add(new SqlParameter("@FNUMBER", faxnumber));
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("SQLエラー: " + ex.Message);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("データベース接続エラー: " + ex.Message);
                    }
                }
            });
        }

    }
}
