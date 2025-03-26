using FileTransfer2ForBlazor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Services
{
    public class ImportLegacyFilesService
    {
        private readonly ArchiveService _archiveService;
        private readonly FileTransferHistoryService _fileTransferHistoryService;
        private readonly string _importTargetDirectory= @"C:\Users\Hinode24\Documents\Test\Documents";
        private readonly string _exportTargetDirectory= @"C:\Users\Hinode24\Documents\Test\ArchiveTest";


        public ImportLegacyFilesService(ArchiveService archiveService, FileTransferHistoryService fileTransferHistoryService)
        { 
            _archiveService = archiveService;
            _fileTransferHistoryService = fileTransferHistoryService;
        }



        public async Task<int> ImportLegacyFiles()
        {
            var tempFilePath = Path.GetTempFileName();
            int result = 0;
            try
            {
                Console.WriteLine("Starting directory write...");
                using (StreamWriter writer = new(tempFilePath))
                {
                    await foreach (var dir in GetDirectoriesStreamAsync(_importTargetDirectory))
                    {
                        Console.WriteLine($"Writing directory: {dir}");
                        await writer.WriteLineAsync(dir);
                    }
                }
                Console.WriteLine($"Finished writing directories to {tempFilePath}");

                var filePrefix = "ImportFolderList_";
                var ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                var fileName = $"{filePrefix}{ts}.csv";
                var fullPath = Path.Combine(@"C:\Users\Hinode24\Documents\Test", fileName);

                Console.WriteLine($"Generating CSV file: {fullPath}");
                using StreamReader reader = new(tempFilePath);
                string folderPath;

                using (var writer = new StreamWriter(fullPath, false, Encoding.GetEncoding("Shift_JIS")))
                {
                    writer.WriteLine("ファイル数,フォルダパス");
                    while ((folderPath = await reader.ReadLineAsync()) != null)
                    {
                        Console.WriteLine($"Processing folder: {folderPath}");
                        int fileCount = await GetFileCountAsync(folderPath);
                        writer.WriteLine($"{fileCount},{folderPath}");
                        await this.InportFiles(folderPath);
                        Console.WriteLine($"Folder: {folderPath}, File Count: {fileCount}");
                        result += fileCount;
                    }
                }
                Console.WriteLine("CSV generation completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during import: {ex.Message}");
                throw; // 再スローしてエラーを呼び出し元に伝える
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                    Console.WriteLine("Temporary file deleted.");
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










        private async Task InportFiles(string folederPath)
        {
            string[] files = [];
            await Task.Run(async () => 
            { 
                files = Directory.GetFiles(folederPath);
                if (files != null)
                {
                    foreach (var file in files)//ここからファイル一件毎の処理
                    {                                               
                        try
                        {
                            var history = new FileTransferHistory
                            {
                                DefaultFullPath = Path.GetFullPath(file)
                            };
                            var directoryTo = _exportTargetDirectory;
                            var fileName = Path.GetFileNameWithoutExtension(file);
                            var extensionName = Path.GetExtension(file);

                            var fileNameArray = fileName.Split('_');
                            //0荷主名_1FAX番号_2タイムスタンプ_3受注ID_4開始日_5終了日_6担当区分_7備考_8金額_9ページ
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
                            newFileName += "_";//金額の分

                            var newPath = directoryTo + @"\" + newFileName + extensionName;
                            int i = 1;
                            while (File.Exists(newPath))
                            {
                                newPath = directoryTo + @"\" + newFileName + "(" + i.ToString() + ")" + extensionName;
                                i++;
                            }
                            var fileInfo = new FileInfo(Path.GetFullPath(file));
                            fileInfo.CopyTo(newPath);

                            history.ArchiveFullPath = newPath;
                            history.ArchiveTime = DateTime.Now;
                            history.ConsignorID = 0;
                            history.ConsignorName = fileNameArray[0];
                            history.FaxNumber = fileNameArray[1];
                            history.TimeStamp = fileNameArray[2];
                            history.StartDate = DateTime.ParseExact(fileNameArray[4], "yyyyMMdd", null);
                            history.StartDate = DateTime.ParseExact(fileNameArray[5], "yyyyMMdd", null);
                            history.Department = fileNameArray[6];
                            history.Remarks = fileNameArray[7];
                            await _fileTransferHistoryService.InsertFileTransferHistoryAsync(history);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(file + ":" + ex.ToString());
                        }
                    }
                }
            });            
        }








    }
}
