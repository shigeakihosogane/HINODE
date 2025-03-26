using FileTransfer2ForBlazor.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Services
{
    public class FileTransferHistoryService
    {
        private readonly string _connectionString;
        private readonly SharedService _sharedService;

        public FileTransferHistoryService(DBConnection connection, SharedService sharedService)
        {
            _connectionString = connection.GetConnectionString();
            _sharedService = sharedService;
        }


        public async Task InsertFileTransferHistoryAsync(FileTransferHistory fileTransferHistory)
        {
            var sql = @"INSERT INTO T_TF_D_FileTransferHistory (
ZyutyuuID, 
DefaultFullPath, 
TemporaryStorageFullPath, 
TemporaryStorageTime, 
TemporaryStorageLimit, 
ArchiveFullPath, 
ArchiveTime,
OrderAmount,
ConsignorID,
ConsignorName,
FaxNumber,
TimeStamp,
StartDate,
EndDate,
Department,
Remarks
) VALUES (
@ZyutyuuID, 
@DefaultFullPath, 
@TemporaryStorageFullPath, 
@TemporaryStorageTime, 
@TemporaryStorageLimit, 
@ArchiveFullPath, 
@ArchiveTime,
@OrderAmount,
@ConsignorID,
@ConsignorName,
@FaxNumber,
@TimeStamp,
@StartDate,
@EndDate,
@Department,
@Remarks
)";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(new SqlParameter("@ZyutyuuID", fileTransferHistory.ZyutyuuID));
                    command.Parameters.Add(new SqlParameter("@DefaultFullPath", fileTransferHistory.DefaultFullPath));
                    command.Parameters.Add(new SqlParameter("@TemporaryStorageFullPath", fileTransferHistory.TemporaryStorageFullPath));
                    command.Parameters.Add(new SqlParameter("@TemporaryStorageTime", (object?)fileTransferHistory.TemporaryStorageTime ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@TemporaryStorageLimit", (object?)fileTransferHistory.TemporaryStorageLimit ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@ArchiveFullPath", fileTransferHistory.ArchiveFullPath));
                    command.Parameters.Add(new SqlParameter("@ArchiveTime", (object?)fileTransferHistory.ArchiveTime ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@OrderAmount", fileTransferHistory.OrderAmount));
                    command.Parameters.Add(new SqlParameter("@ConsignorID", fileTransferHistory.ConsignorID));
                    command.Parameters.Add(new SqlParameter("@ConsignorName", fileTransferHistory.ConsignorName));
                    command.Parameters.Add(new SqlParameter("@FaxNumber", fileTransferHistory.FaxNumber));
                    command.Parameters.Add(new SqlParameter("@TimeStamp", fileTransferHistory.TimeStamp));
                    command.Parameters.Add(new SqlParameter("@StartDate", (object?)fileTransferHistory.StartDate ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@EndDate", (object?)fileTransferHistory.EndDate ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Department", fileTransferHistory.Department));
                    command.Parameters.Add(new SqlParameter("@Remarks", fileTransferHistory.Remarks));

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
                    Console.WriteLine("InsertFileTransferHistoryAsync: " + ex.Message);
                }
            }
            var external = new ExternalClass(_sharedService);
            await external.CallGetFileTransferHistoryAsync();
        }



        public async Task<List<FileTransferHistory>> GetFileTransferHistoryAsync(int logRetrievalCount)
        {
            var fileTransferHistorys = new List<FileTransferHistory>();
            var sql = $@"SELECT TOP {logRetrievalCount}                     dbo.T_TF_D_FileTransferHistory.*
FROM                         dbo.T_TF_D_FileTransferHistory
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
                            var fileTransferHistory = new FileTransferHistory
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ZyutyuuID = Convert.ToDecimal(reader["ZyutyuuID"]),
                                DefaultFullPath = Convert.ToString(reader["DefaultFullPath"]) ?? "",
                                TemporaryStorageFullPath = Convert.ToString(reader["TemporaryStorageFullPath"]) ?? "",
                                TemporaryStorageTime = reader["TemporaryStorageTime"] != DBNull.Value ? Convert.ToDateTime(reader["TemporaryStorageTime"]) : null,
                                TemporaryStorageLimit = reader["TemporaryStorageLimit"] != DBNull.Value ? Convert.ToDateTime(reader["TemporaryStorageLimit"]) : null,
                                ArchiveFullPath = Convert.ToString(reader["ArchiveFullPath"]) ?? "",
                                ArchiveTime = reader["ArchiveTime"] != DBNull.Value ? Convert.ToDateTime(reader["ArchiveTime"]) : null,
                                OrderAmount = Convert.ToDecimal(reader["OrderAmount"]),
                                ConsignorID = Convert.ToInt32(reader["ConsignorID"]),
                                ConsignorName = Convert.ToString(reader["ConsignorName"]) ?? "",
                                FaxNumber = Convert.ToString(reader["FaxNumber"]) ?? "",
                                TimeStamp = Convert.ToString(reader["TimeStamp"]) ?? "",
                                StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : null,
                                EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : null,
                                Department = Convert.ToString(reader["Department"]) ?? "",
                                Remarks = Convert.ToString(reader["Remarks"]) ?? ""
                            };
                            fileTransferHistorys.Add(fileTransferHistory);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQLエラー: " + ex.Message);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("GetFileTransferHistoryAsync: " + ex.Message);
                }
            }
            return fileTransferHistorys;
        }

        public async Task UpdateFileTransferHistoryAsync(FileTransferHistory fileTransferHistory)
        {
            var sql = @"UPDATE T_TF_D_FileTransferHistory 
SET ArchiveFullPath = @ArchiveFullPath,
    ArchiveTime = @ArchiveTime 
WHERE (Id = @Id)";
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(new SqlParameter("@ArchiveFullPath", fileTransferHistory.ArchiveFullPath));
                    command.Parameters.Add(new SqlParameter("@ArchiveTime", fileTransferHistory.ArchiveTime));
                    command.Parameters.Add(new SqlParameter("@Id", fileTransferHistory.Id));
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
                    Console.WriteLine("UpdateFileTransferHistoryAsync: " + ex.Message);
                }
            }
            var external = new ExternalClass(_sharedService);
            await external.CallGetFileTransferHistoryAsync();
        }









    }
}
