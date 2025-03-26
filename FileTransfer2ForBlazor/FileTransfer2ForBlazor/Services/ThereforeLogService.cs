using FileTransfer2ForBlazor.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Services
{
    public class ThereforeLogService
    {
        private readonly string _connectionString;

        public ThereforeLogService()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"SV201710";
            builder.InitialCatalog = "HINODEDB";
            builder.UserID = "sa";
            builder.Password = "cube%6614";
            builder.TrustServerCertificate = true;
            _connectionString = builder.ToString();
        }



        public async Task AddThereforeLogAsync(FileTransferLog fileTranceferLog)
        {            
            var sql = @"INSERT INTO T_TF_D_FileTransferLog (
処理, 
結果, 
変更前ファイル名, 
変更後ファイル名, 
日時
) VALUES (
@処理, 
@結果, 
@変更前ファイル名, 
@変更後ファイル名, 
@日時)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            try
            {
                await connection.OpenAsync();
                command.Parameters.Add(new SqlParameter("@処理", fileTranceferLog.Process));
                command.Parameters.Add(new SqlParameter("@結果", fileTranceferLog.Result));
                command.Parameters.Add(new SqlParameter("@変更前ファイル名", Path.GetFileName(fileTranceferLog.FullPathBeforeTransfer)));
                command.Parameters.Add(new SqlParameter("@変更後ファイル名", Path.GetFileName(fileTranceferLog.FullPathAfterTransfer)));
                command.Parameters.Add(new SqlParameter("@日時", (object?)fileTranceferLog.ProcessedDateTime ?? DBNull.Value));
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQLエラー: " + ex.Message);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("InsertFileTranceferLogAsync: " + ex.Message);
            }
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
