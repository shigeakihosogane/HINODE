using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WindowsFormsApp2.Models;

namespace WindowsFormsApp2.Services
{
    public static class SettingService
    {
        private static String _connectionString;

        static SettingService()
        {            
            _connectionString = DBConnection.GetConnectionString();
        }

        public async static Task<Setting> GetSettingAsync()
        {
            Setting setting = new Setting();
            var sql = @"SELECT                      TOP (1) Id, 更新日時, 転送1_監視, 転送1_転送, 転送1_戻し, 転送1_間隔, 転送2_監視, 転送2_転送, 転送2_戻し, 転送2_間隔, ログ_表示件数, アーカイブ先, 経過日数 
FROM                         dbo.T_TF_D_Setting
WHERE Id = (select MAX(Id) FROM dbo.T_TF_D_Setting)";

            await Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            try
                            {
                                while (reader.Read())
                                {
                                    setting.転送1_監視 = Convert.ToString(reader["転送1_監視"]);
                                    setting.転送1_転送 = Convert.ToString(reader["転送1_転送"]);
                                    setting.転送1_戻し = Convert.ToString(reader["転送1_戻し"]);
                                    setting.転送1_間隔 = Convert.ToInt32(reader["転送1_間隔"]);
                                    setting.転送2_監視 = Convert.ToString(reader["転送2_監視"]);
                                    setting.転送2_転送 = Convert.ToString(reader["転送2_転送"]);
                                    setting.転送2_戻し = Convert.ToString(reader["転送2_戻し"]);
                                    setting.転送2_間隔 = Convert.ToInt32(reader["転送2_間隔"]);
                                    setting.ログ_表示件数 = Convert.ToInt32(reader["ログ_表示件数"]);
                                    setting.アーカイブ先 = Convert.ToString(reader["アーカイブ先"]);
                                    setting.経過日数 = Convert.ToInt32(reader["経過日数"]);
                                }
                            }
                            catch (Exception ex)
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
            return setting;
        }

        public async static Task UpdateSettingAsync(Setting setting)
        {
            var sql = @"INSERT INTO T_TF_D_Setting (
更新日時,
転送1_監視,
転送1_転送,
転送1_戻し,
転送1_間隔,
転送2_監視,
転送2_転送,
転送2_戻し,
転送2_間隔,
ログ_表示件数,
アーカイブ先,
経過日数
) VALUES (
@更新日時,
@転送1_監視,
@転送1_転送,
@転送1_戻し,
@転送1_間隔,
@転送2_監視,
@転送2_転送,
@転送2_戻し,
@転送2_間隔,
@ログ_表示件数,
@アーカイブ先,
@経過日数)";

            await Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("@更新日時", DateTime.Now));
                        command.Parameters.Add(new SqlParameter("@転送1_監視", setting.転送1_監視));
                        command.Parameters.Add(new SqlParameter("@転送1_転送", setting.転送1_転送));
                        command.Parameters.Add(new SqlParameter("@転送1_戻し", setting.転送1_戻し));
                        command.Parameters.Add(new SqlParameter("@転送1_間隔", setting.転送1_間隔));
                        command.Parameters.Add(new SqlParameter("@転送2_監視", setting.転送2_監視));
                        command.Parameters.Add(new SqlParameter("@転送2_転送", setting.転送2_転送));
                        command.Parameters.Add(new SqlParameter("@転送2_戻し", setting.転送2_戻し));
                        command.Parameters.Add(new SqlParameter("@転送2_間隔", setting.転送2_間隔));
                        command.Parameters.Add(new SqlParameter("@ログ_表示件数", setting.ログ_表示件数));
                        command.Parameters.Add(new SqlParameter("@アーカイブ先", setting.アーカイブ先));
                        command.Parameters.Add(new SqlParameter("@経過日数", setting.経過日数));
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
