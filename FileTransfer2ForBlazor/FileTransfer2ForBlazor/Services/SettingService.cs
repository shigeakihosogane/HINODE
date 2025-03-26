using FileTransfer2ForBlazor.Components.Pages;
using FileTransfer2ForBlazor.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Setting = FileTransfer2ForBlazor.Models.Setting;

namespace FileTransfer2ForBlazor.Services
{
    public class SettingService
    {
        private readonly string _connectionString;

        public SettingService(DBConnection connection)
        {
            _connectionString = connection.GetConnectionString();
        }


        public async Task<Setting> GetSetting(string serialNumber)
        {
            Setting setting = new();
            var now = DateTime.Now;
            var sql = @"SELECT                      dbo.T_TF_D_Setting.*
FROM                         dbo.T_TF_D_Setting
WHERE                       (SerialNumber = @SerialNumber)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));                    

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            try
                            {
                                while (await reader.ReadAsync())
                                {
                                    setting.Trans1Monitoring = Convert.ToString(reader["Trans1Monitoring"]) ?? "";
                                    setting.Trans1Successful = Convert.ToString(reader["Trans1Successful"]) ?? "";
                                    setting.Trans2Monitoring = Convert.ToString(reader["Trans2Monitoring"]) ?? "";
                                    setting.Trans2Successful = Convert.ToString(reader["Trans2Successful"]) ?? "";
                                    setting.Trans2Error = Convert.ToString(reader["Trans2Error"]) ?? "";
                                    setting.TfUploader = Convert.ToString(reader["TfUploader"]) ?? "";
                                    setting.ArchiveFolder = Convert.ToString(reader["ArchiveFolder"]) ?? "";
                                    setting.FileListFolder = Convert.ToString(reader["FileListFolder"]) ?? "";
                                    setting.TransferQueueTime = Convert.ToInt32(reader["TransferQueueTime"]);
                                    setting.MonitoringInterval = Convert.ToInt32(reader["MonitoringInterval"]);
                                    setting.ScheduledExecutionTime = Convert.ToDateTime(reader["ScheduledExecutionTime"]);
                                    setting.LogRetrievalCount = Convert.ToInt32(reader["LogRetrievalCount"]);                                    
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"エラーの詳細: {ex.Message}");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("GetIndexAsync: " + ex.Message);
                }
            }
            return setting;
        }            


        public async Task SaveSettingAsync(string serialNumber, Setting setting)
        {
            var sql= @"
MERGE INTO [dbo].[T_TF_D_Setting] AS Target
USING (VALUES 
    (@SerialNumber, @Trans1Monitoring, @Trans1Successful, @Trans2Monitoring, @Trans2Successful, @Trans2Error, 
     @TfUploader, @ArchiveFolder, @FileListFolder, @TransferQueueTime, @MonitoringInterval, @ScheduledExecutionTime, @LogRetrievalCount)
) AS Source (
    SerialNumber, Trans1Monitoring, Trans1Successful, Trans2Monitoring, Trans2Successful, Trans2Error,
    TfUploader, ArchiveFolder, FileListFolder, TransferQueueTime, MonitoringInterval, ScheduledExecutionTime, LogRetrievalCount
)
ON Target.[SerialNumber] = Source.[SerialNumber]
WHEN MATCHED THEN
    UPDATE SET 
        [Trans1Monitoring] = Source.[Trans1Monitoring],
        [Trans1Successful] = Source.[Trans1Successful],
        [Trans2Monitoring] = Source.[Trans2Monitoring],
        [Trans2Successful] = Source.[Trans2Successful],
        [Trans2Error] = Source.[Trans2Error],
        [TfUploader] = Source.[TfUploader],
        [ArchiveFolder] = Source.[ArchiveFolder],
        [FileListFolder] = Source.[FileListFolder],
        [TransferQueueTime] = Source.[TransferQueueTime],
        [MonitoringInterval] = Source.[MonitoringInterval],
        [ScheduledExecutionTime] = Source.[ScheduledExecutionTime],
        [LogRetrievalCount] = Source.[LogRetrievalCount]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([SerialNumber], [Trans1Monitoring], [Trans1Successful], [Trans2Monitoring], [Trans2Successful], [Trans2Error],
            [TfUploader], [ArchiveFolder], [FileListFolder], [TransferQueueTime], [MonitoringInterval], [ScheduledExecutionTime], [LogRetrievalCount])
    VALUES (Source.[SerialNumber], Source.[Trans1Monitoring], Source.[Trans1Successful], Source.[Trans2Monitoring], Source.[Trans2Successful], Source.[Trans2Error],
            Source.[TfUploader], Source.[ArchiveFolder], Source.[FileListFolder], Source.[TransferQueueTime], Source.[MonitoringInterval], 
            Source.[ScheduledExecutionTime], Source.[LogRetrievalCount]);";

            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using (SqlTransaction transaction = connection.BeginTransaction())
            using (SqlCommand command = new SqlCommand(sql, connection, transaction))
            try
            {
                command.Parameters.Add(new SqlParameter("@SerialNumber", serialNumber));
                command.Parameters.Add(new SqlParameter("@Trans1Monitoring", setting.Trans1Monitoring));
                command.Parameters.Add(new SqlParameter("@Trans1Successful", setting.Trans1Successful));
                command.Parameters.Add(new SqlParameter("@Trans2Monitoring", setting.Trans2Monitoring));
                command.Parameters.Add(new SqlParameter("@Trans2Successful", setting.Trans2Successful));
                command.Parameters.Add(new SqlParameter("@Trans2Error", setting.Trans2Error));
                command.Parameters.Add(new SqlParameter("@TfUploader", setting.TfUploader));
                command.Parameters.Add(new SqlParameter("@ArchiveFolder", setting.ArchiveFolder));
                command.Parameters.Add(new SqlParameter("@FileListFolder", setting.FileListFolder));
                command.Parameters.Add(new SqlParameter("@TransferQueueTime", setting.TransferQueueTime));
                command.Parameters.Add(new SqlParameter("@MonitoringInterval", setting.MonitoringInterval));
                command.Parameters.Add(new SqlParameter("@ScheduledExecutionTime", setting.ScheduledExecutionTime));
                command.Parameters.Add(new SqlParameter("@LogRetrievalCount", setting.LogRetrievalCount));
                                        
                int rowsAffected = await command.ExecuteNonQueryAsync();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
                    
            }
        }






    }
}
