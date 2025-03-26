using FileTransfer2ForBlazor.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Services
{
    public class FileRegistryService
    {
        private readonly string _connectionString;
        private readonly SharedService _sharedService;

        public FileRegistryService(DBConnection connection, SharedService sharedService)
        {
            _connectionString = connection.GetConnectionString(); 
            _sharedService = sharedService;
        }


        public async Task InsertFileRegistryAsync(FileRegistry fileRegistry)
        {
            var sql = @"INSERT INTO dbo.T_TF_D_FileRegistry (
FileFullPath,
Thumbnail,
CreatedDate,
ZyutyuuID,
ConsignorName,
Department,
Remarks,
StartDate,
EndDate,
OrderAmount,
RootDirectory,
DirectoryPath,
FileName
) VALUES (
@FileFullPath,
@Thumbnail,
@CreatedDate,
@ZyutyuuID,
@ConsignorName,
@Department,
@Remarks,
@StartDate,
@EndDate,
@OrderAmount,
@RootDirectory,
@DirectoryPath,
@FileName
)";
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            try
            {
                await connection.OpenAsync();
                command.Parameters.Add(new SqlParameter("@FileFullPath", fileRegistry.FileFullPath));
                command.Parameters.Add(new SqlParameter("@Thumbnail", fileRegistry.Thumbnail));
                command.Parameters.Add(new SqlParameter("@CreatedDate", fileRegistry.CreatedDate));
                command.Parameters.Add(new SqlParameter("@ZyutyuuID", fileRegistry.ZyutyuuID));
                command.Parameters.Add(new SqlParameter("@ConsignorName", fileRegistry.ConsignorName));
                command.Parameters.Add(new SqlParameter("@Department", fileRegistry.Department)); 
                command.Parameters.Add(new SqlParameter("@Remarks", fileRegistry.Remarks));
                command.Parameters.Add(new SqlParameter("@StartDate", fileRegistry.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", fileRegistry.EndDate));
                command.Parameters.Add(new SqlParameter("@OrderAmount", fileRegistry.OrderAmount));
                command.Parameters.Add(new SqlParameter("@RootDirectory", fileRegistry.RootDirectory));
                command.Parameters.Add(new SqlParameter("@DirectoryPath", fileRegistry.DirectoryPath));
                command.Parameters.Add(new SqlParameter("@FileName", fileRegistry.FileName));
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
            var external = new ExternalClass(_sharedService);
            await external.CallGetFileRegistryAsync();
        }

        public async Task<List<FileRegistry>> GetFileRegistryAsync(int logRetrievalCount)
        {
            var fileRegistrys = new List<FileRegistry>();
            var sql = $@"SELECT                      TOP ({logRetrievalCount}) dbo.T_TF_D_FileRegistry.*
FROM                         dbo.T_TF_D_FileRegistry
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
                            var fileRegistry = new FileRegistry
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FileFullPath = Convert.ToString(reader["FileFullPath"]) ?? "",
                                Thumbnail = reader["Thumbnail"]!=DBNull.Value ? reader["Thumbnail"] as Byte[] : null,
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                ZyutyuuID = Convert.ToDecimal(reader["ZyutyuuID"]),
                                ConsignorName = Convert.ToString(reader["ConsignorName"]) ?? "",
                                Department = Convert.ToString(reader["Department"]) ?? "",
                                Remarks = Convert.ToString(reader["Remarks"]) ?? "",
                                StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : null,                                
                                EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : null,
                                OrderAmount = Convert.ToDecimal(reader["OrderAmount"]),
                                RootDirectory = Convert.ToString(reader["RootDirectory"]) ?? "",
                                DirectoryPath = Convert.ToString(reader["DirectoryPath"]) ?? "",
                                FileName = Convert.ToString(reader["FileName"]) ?? ""

                            };
                            fileRegistrys.Add(fileRegistry);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQLエラー: " + ex.Message);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("GetFileRegistryAsync: " + ex.Message);
                }
            }
            return fileRegistrys;
        }








    }
}
