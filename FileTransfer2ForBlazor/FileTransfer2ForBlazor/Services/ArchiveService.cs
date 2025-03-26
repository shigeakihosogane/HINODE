using FileTransfer2ForBlazor.Components.Pages;
using FileTransfer2ForBlazor.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace FileTransfer2ForBlazor.Services
{
    public class ArchiveService
    {
        private readonly string _connectionString;
        private readonly SettingService _settingService;
        private readonly string _serialNumber;
        private readonly FileTransferHistoryService _fileTransferHistoryService;
        private readonly FileTransferLogService _fileTransferLogService;
        public ArchiveService(DBConnection connection, SettingService settingService, MotherboardIDService motherboardIDService,
            FileTransferHistoryService fileTransferHistoryService, FileTransferLogService fileTransferLogService)
        {
            _connectionString = connection.GetConnectionString();
            _settingService = settingService;
            _serialNumber = motherboardIDService.GetMotherboardSerialNumber();
            _fileTransferHistoryService = fileTransferHistoryService;
            _fileTransferLogService = fileTransferLogService;
        }


        public async Task<int> ArchiveProcessedFile()//処理完了ファイル
        {            
            var result = 0;            
            var sql = @"SELECT                      dbo.T_TF_D_FileTransferHistory.*
FROM                         dbo.T_TF_D_FileTransferHistory
WHERE                       (TemporaryStorageLimit IS NOT NULL) AND (TemporaryStorageLimit < @DT) AND (ArchiveFullPath = N'')";


            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(new SqlParameter("@DT", DateTime.Now));
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
                                OrderAmount = Convert.ToDecimal(reader["OrderAmount"])
                            };
                            result++;

                            var fileTransferLog = new FileTransferLog
                            {
                                Process = "最終保管",
                                FullPathBeforeTransfer = fileTransferHistory.TemporaryStorageFullPath
                            };

                            await this.MoveToArchive(fileTransferHistory, fileTransferLog);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("ArchiveProcessedFile: " + ex.Message);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("ArchiveProcessedFile: " + ex.Message);
                }
            }
            return result;
               
        }
       

        public async Task<int> ArchiveExpiredFile()//期限切れファイル
        {
            await Task.Delay(1000);








            return 0;
        }



        private async Task MoveToArchive(FileTransferHistory fileTransferHistory, FileTransferLog fileTransferLog)
        {
            string result;
            string newPath;

            if (!File.Exists(fileTransferHistory.TemporaryStorageFullPath))
            {
                result = "失敗";
                newPath = "一時保管ファイルが見つかりません。";
            }
            else
            {                
                var setting = await _settingService.GetSetting(_serialNumber);
                var directoryTo = setting.ArchiveFolder;
                var fullPath = Path.GetFullPath(fileTransferHistory.TemporaryStorageFullPath);
                var fileName = Path.GetFileNameWithoutExtension(fullPath);
                var extensionName = Path.GetExtension(fullPath);

                var fileNameArray = fileName.Split('_');
                directoryTo += @"\" + fileNameArray[0];
                directoryTo += @"\" + fileNameArray[6];
                if (fileNameArray[6] == "4.倉庫保管")
                {
                    directoryTo += @"\" + fileNameArray[7]; ;
                }
                if (fileNameArray[4].Length == 8)
                {
                    directoryTo += string.Concat(@"\", fileNameArray[4].AsSpan(0, 6));                    
                }
                if (!Directory.Exists(directoryTo))
                {
                    Directory.CreateDirectory(directoryTo);
                }
                var newFileName = "";
                for (var n = 0; n < 8; n++)
                {
                    newFileName += fileNameArray[n] + "_"; 
                }
                newFileName += fileTransferHistory.OrderAmount.ToString() + "_";
                newPath = directoryTo + @"\" + newFileName + extensionName;
                int i = 1;
                while (File.Exists(newPath))
                {
                    newPath = directoryTo + @"\" + newFileName + "(" + i.ToString() + ")" + extensionName;
                    i++;
                }

                try//転送、History更新
                {
                    var fileInfo = new FileInfo(fullPath);                    
                    fileInfo.MoveTo(newPath);
                    result = "正常";
                }
                catch(Exception ex)
                {
                    result = "例外";
                    newPath = ex.Message;
                }
            }

            try
            {
                var ts = DateTime.Now;
                fileTransferLog.Result = result;
                fileTransferLog.FullPathAfterTransfer = newPath;
                fileTransferLog.ProcessedDateTime = ts;
                await _fileTransferLogService.InsertFileTranceferLogAsync(fileTransferLog);

                fileTransferHistory.ArchiveFullPath = newPath;
                fileTransferHistory.ArchiveTime = ts;
                await _fileTransferHistoryService.UpdateFileTransferHistoryAsync(fileTransferHistory);
            }
            catch (Exception ex)
            {
                Console.WriteLine("MoveToArchive" + ex.Message);
            }
            
            
        }








        public async Task<int> GetTemporaryStorageTotalCountAsync(string rootPath)
        {            
            string tempFilePath = Path.GetTempFileName();
            int result = 0;
            try
            {
                // 一時ファイルにフォルダパスを書き出す（非同期）
                using (StreamWriter writer = new(tempFilePath))
                {
                    await foreach (var dir in GetDirectoriesStreamAsync(rootPath))
                    {
                        await writer.WriteLineAsync(dir);
                    }
                }

                Console.WriteLine($"フォルダパスを書き出しました: {tempFilePath}");

                var setting = await _settingService.GetSetting(_serialNumber);
                var targetFolder = setting.FileListFolder;
                var filePrefix = "ファイルリスト_";
                var ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                var fileName = $"{filePrefix}{ts}.csv";
                var fullPath = Path.Combine(targetFolder, fileName);

                // 一時ファイルからフォルダパスを読み込み、ファイル数をカウント（非同期）
                using StreamReader reader = new(tempFilePath);
                string folderPath;

                using (var writer = new StreamWriter(fullPath,false, Encoding.GetEncoding("Shift_JIS")))
                {
                    writer.WriteLine("ファイル数,フォルダパス");
                    while ((folderPath = await reader.ReadLineAsync()) != null)
                    {
                        int fileCount = await GetFileCountAsync(folderPath);
                        writer.WriteLine($"{fileCount},{folderPath}");
                        Console.WriteLine($"Folder: {folderPath}, File Count: {fileCount}");
                        result += fileCount;
                    }
                }
            }
            finally
            {
                // 一時ファイルを削除
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                    Console.WriteLine("一時ファイルを削除しました。");
                }
            }
            return result;
        }

        private static async IAsyncEnumerable<string> GetDirectoriesStreamAsync(string path)
        {
            Queue<string> directories = new();
            directories.Enqueue(path);

            while (directories.Count > 0)
            {
                string currentDir = directories.Dequeue();
                yield return currentDir;

                string[] subDirs;
                try
                {
                    subDirs = await Task.Run(() => Directory.GetDirectories(currentDir));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accessing {currentDir}: {ex.Message}");
                    continue;
                }

                foreach (var subDir in subDirs)
                {
                    directories.Enqueue(subDir);
                }
            }
        }

        private static async Task<int> GetFileCountAsync(string folderPath)
        {
            try
            {
                return await Task.Run(() => Directory.GetFiles(folderPath).Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error counting files in {folderPath}: {ex.Message}");
                return 0;
            }
        }



    }
}
