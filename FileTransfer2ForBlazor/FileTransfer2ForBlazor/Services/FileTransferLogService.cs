using FileTransfer2ForBlazor.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Services
{
    public class FileTransferLogService
    {
        private readonly string _connectionString;
        private readonly SharedService _sharedService;
        public FileTransferLogService(DBConnection connection, SharedService sharedService)
        {
            _connectionString = connection.GetConnectionString();
            _sharedService = sharedService;
        }


        public async Task InsertFileTranceferLogAsync(FileTransferLog fileTransferLog)
        {
            var sql = @"INSERT INTO T_TF_D_FileTransferLog (
Process, 
Result, 
FullPathBeforeTransfer, 
FullPathAfterTransfer, 
ProcessedDateTime
) VALUES (
@Process, 
@Result, 
@FullPathBeforeTransfer, 
@FullPathAfterTransfer, 
@ProcessedDateTime
)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(new SqlParameter("@Process", fileTransferLog.Process));
                    command.Parameters.Add(new SqlParameter("@Result", fileTransferLog.Result));
                    command.Parameters.Add(new SqlParameter("@FullPathBeforeTransfer", fileTransferLog.FullPathBeforeTransfer));
                    command.Parameters.Add(new SqlParameter("@FullPathAfterTransfer", fileTransferLog.FullPathAfterTransfer));
                    command.Parameters.Add(new SqlParameter("@ProcessedDateTime", (object?)fileTransferLog.ProcessedDateTime ?? DBNull.Value));
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
                    Console.WriteLine("InsertFileTransferLogAsync: " + ex.Message);
                }
            }
            var external = new ExternalClass(_sharedService);
            await external.CallGetFileTranceferLogAsync();
        }


        public async Task<List<FileTransferLog>> GetFileTranceferLogAsync(int logRetrievalCount)
        {
            var fileTransferLogs = new List<FileTransferLog>();
            var sql = $@"SELECT TOP {logRetrievalCount}                     dbo.T_TF_D_FileTransferLog.*
FROM                         dbo.T_TF_D_FileTransferLog
ORDER BY               Id DESC";


            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    await connection.OpenAsync();                    
                    using var reader = await command.ExecuteReaderAsync();
                    try
                    {
                        while (await reader.ReadAsync())
                        {
                            var fileTransferLog = new FileTransferLog
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Process = Convert.ToString(reader["Process"]) ?? "",
                                Result = Convert.ToString(reader["Result"]) ?? "",
                                FullPathBeforeTransfer = Convert.ToString(reader["FullPathBeforeTransfer"]) ?? "",
                                FullPathAfterTransfer = Convert.ToString(reader["FullPathAfterTransfer"]) ?? "",
                                ProcessedDateTime = reader["ProcessedDateTime"] != DBNull.Value ? Convert.ToDateTime(reader["ProcessedDateTime"]) : null,
                            };
                            fileTransferLogs.Add(fileTransferLog);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQLエラー: " + ex.Message);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("GetFileTranceferLogAsync: " + ex.Message);
                }
            }
            return fileTransferLogs;
        }









    }
}
