using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Models;

namespace WindowsFormsApp2.Services
{
    public class TranceferLogService
    {
        private static String _connectionString;
        static TranceferLogService()
        {
            _connectionString = DBConnection.GetConnectionString();
        }

        public async static Task<BindingList<TranceferLog>> GetTranceferLogAsync(int kennsuu)
        {
            var logs = new BindingList<TranceferLog>();
            var sql = @"SELECT                      TOP (@KEN) 
ID, 
処理, 
結果, 
変更前Fullpath, 
変更後Fullpath, 
日時
FROM                         dbo.T_TF_D_TranceferLog
ORDER BY               ID DESC";
            await Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("@KEN", kennsuu));
                        using (var reader = command.ExecuteReader())
                        {
                            try
                            {
                                while (reader.Read())
                                {
                                    logs.Add(new TranceferLog(
                                        Convert.ToInt32(reader["ID"]),
                                        Convert.ToString(reader["処理"]),
                                        Convert.ToString(reader["結果"]),
                                        Path.GetFileName(Convert.ToString(reader["変更前Fullpath"])),
                                        Path.GetFileName(Convert.ToString(reader["変更後Fullpath"])),
                                        Convert.ToDateTime(reader["日時"])
                                        ));
                                }
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine("SQLエラー: " + ex.Message);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("データベース接続エラー: " + ex.Message);
                    }
                }
            });
            return logs;
        }

        public async static Task AddTranceferLogAsync(TranceferLog tranceferLog)
        {
            var sql = @"INSERT INTO T_TF_D_TranceferLog (
処理,
結果,
変更前Fullpath,
変更後Fullpath,
日時
) VALUES (
@処理,
@結果,
@変更前Fullpath,
@変更後Fullpath,
@日時)";

            await Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("@処理", tranceferLog.処理));
                        command.Parameters.Add(new SqlParameter("@結果", tranceferLog.結果));
                        command.Parameters.Add(new SqlParameter("@変更前Fullpath", tranceferLog.変更前Fullpath));
                        command.Parameters.Add(new SqlParameter("@変更後Fullpath", tranceferLog.変更後Fullpath));
                        command.Parameters.Add(new SqlParameter("@日時", tranceferLog.日時));
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
