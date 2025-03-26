using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Models;

namespace WindowsFormsApp2.Services
{
    public static class FaxdataRegistrationHistoryService
    {
        private static String _connectionString;

        static FaxdataRegistrationHistoryService()
        {
            _connectionString = DBConnection.GetConnectionString();
        }

        public async static Task InsertFaxdataAsync(FaxdataRegistrationHistory faxdataRegistrationHistory)
        {
            var sql = @"INSERT INTO T_TF_D_FaxdataRegistrationHistory (
ZyutyuuID,
CreateDateTime,
TempFullPath,
ArchiveFullPath,
ArchiveDateTime
) VALUES (
@ZyutyuuID,
@CreateDateTime,
@TempFullPath,
@ArchiveFullPath,
@ArchiveDateTime
)";
            await Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("@ZyutyuuID", faxdataRegistrationHistory.ZyutyuuID));
                        command.Parameters.Add(new SqlParameter("@CreateDateTime", faxdataRegistrationHistory.CreateDateTime));
                        command.Parameters.Add(new SqlParameter("@TempFullPath", faxdataRegistrationHistory.TempFullPath));
                        command.Parameters.Add(new SqlParameter("@ArchiveFullPath", faxdataRegistrationHistory.ArchiveFullPath));
                        command.Parameters.Add(new SqlParameter("@ArchiveDateTime", faxdataRegistrationHistory.ArchiveDateTime));
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
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
