using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FileTransfer2ForBlazor.Components.Pages;
using FileTransfer2ForBlazor.Models;
using ImageMagick;
using Microsoft.VisualBasic.Logging;
using PdfiumViewer;
using Index = FileTransfer2ForBlazor.Models.Index;
using System.Security.Cryptography;

namespace FileTransfer2ForBlazor.Services
{
    public class FileTransferService
    {
        private readonly SettingService _settingService;
        private readonly string _serialNumber;
        private readonly IndexService _indexService;
        private readonly NinusiInfoService _ninusiInfoService;
        private readonly FileTransferHistoryService _fileTransferHistoryService;
        private readonly FileTransferLogService _fileTransferLogService;
        private readonly ThereforeLogService _thereforeLogService;
        private readonly FileRegistryService _fileRegistryService;
        private readonly DocumentIndexService _documentIndexService;

        public FileTransferService(SettingService settingService, IndexService indexService, NinusiInfoService ninusiInfoService, MotherboardIDService motherboardIDService,
            FileTransferHistoryService fileTransferHistoryService, FileTransferLogService fileTransferLogService, ThereforeLogService thereforeLogService, 
            FileRegistryService fileRegistryService, DocumentIndexService documentIndexService)
        {
            _settingService = settingService;
            _serialNumber = motherboardIDService.GetMotherboardSerialNumber();
            _indexService = indexService;
            _ninusiInfoService = ninusiInfoService;
            _fileTransferHistoryService = fileTransferHistoryService;
            _fileTransferLogService = fileTransferLogService;
            _thereforeLogService = thereforeLogService;
            _fileRegistryService = fileRegistryService;
            _documentIndexService = documentIndexService;
        }


        public async Task AtherFileFormat(string file,bool cubeState)
        {
            var setting = await _settingService.GetSetting(_serialNumber);                           
            var fullPath = Path.GetFullPath(file);
            var fileName = Path.GetFileNameWithoutExtension(file);
            var extensionName = Path.GetExtension(file);
            //以下全部一緒
            //this.txtAPPログ.Text += "\r\n" + file;
            //this.txtAPPログ.Text += "\r\n" + fullPath;
            //this.txtAPPログ.Text += "\r\n" + directoryName+ @"\" + fileName + extensionName;

            var fileTransferLog = new FileTransferLog
            {
                FullPathBeforeTransfer = fullPath,
                Process = "取込処理"
            };

            var directryTo = setting.Trans1Successful;
            string newFileName;                
            if (DateTime.TryParseExact(fileName, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                newFileName = "scan__" + dt.ToString("yyyyMMddHHmmss");
            }
            else
            {
                newFileName = "取込__" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            int i = 1;
            var newFullPath = directryTo + @"\" + newFileName + extensionName;
            while (File.Exists(newFullPath))
            {
                newFullPath = directryTo + @"\" + newFileName + "(" + i.ToString() + ")" + extensionName;
                i++;
            }
            try
            {
                var fileInfo = new FileInfo(fullPath);
                fileInfo.MoveTo(newFullPath);
                fileTransferLog.Result = "正常";
                fileTransferLog.ProcessedDateTime = DateTime.Now;
                fileTransferLog.FullPathAfterTransfer = newFullPath;
                await _fileTransferLogService.InsertFileTranceferLogAsync(fileTransferLog);
                //======================================================================================Therefore用転送
                if (cubeState)//========================================================================
                {//=====================================================================================
                    await _thereforeLogService.AddThereforeLogAsync(fileTransferLog);//=================
                }//=====================================================================================
                //======================================================================================

            }
            catch (Exception e)
            {
                Console.WriteLine(e);                    
            }
            
        }





        public async Task GenerateFileNameWithIndex(string file, bool cubeState)
        {
            var fullPath = Path.GetFullPath(file);
            //var directoryName = Path.GetDirectoryName(file);
            var fileName = Path.GetFileNameWithoutExtension(file);
            var extensionName = Path.GetExtension(file);
            //以下全部一緒
            //this.txtAPPログ.Text += "\r\n" + file;
            //this.txtAPPログ.Text += "\r\n" + fullPath;
            //this.txtAPPログ.Text += "\r\n" + directoryName+ @"\" + fileName + extensionName;

            DateTime dt;
            var emsg = "";
            var fNameArray = fileName.Split('_');


            var fileNameElement = new FileNameElement
            {
                拡張子 = extensionName,
                元ファイルFullPath = fullPath
            };
            var fileTransferHistory = new FileTransferHistory
            {
                DefaultFullPath = fullPath,
                OrderAmount = 0
            };
            var fileTransferLog = new FileTransferLog
            {
                Process = "一時保管",
                FullPathBeforeTransfer = fullPath
            };
            try
            {
                //pdf以外は戻す
                if (extensionName != ".pdf")
                {
                    //戻し転送処理
                    await this.MoveToErrorDirectory(fullPath, emsg, fileTransferLog, cubeState);                    
                    return;
                }

                if (decimal.TryParse(fNameArray[0], out decimal zyutyuuID))//
                {
                    var index = new Index();
                    index = await _indexService.GetIndexAsync(zyutyuuID);
                    fileTransferHistory.ZyutyuuID = zyutyuuID;
                    
                    //タイムスタンプ位置を取得
                    int len = fNameArray.Length;
                    int tsPosition;
                    for (tsPosition = 0; tsPosition < len - 1; tsPosition++)
                    {
                        var str = fNameArray[tsPosition];
                        if (str.Length >= 14)
                        {
                            if (DateTime.TryParseExact(fNameArray[tsPosition][..14], "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
                            {
                                break;
                            }
                        }
                    }

                    if (index.ID != 0)
                    {
                        //受注ファイル以外
                        if (zyutyuuID == 501 || zyutyuuID == 601 || zyutyuuID == 701 || zyutyuuID == 801 || zyutyuuID == 901)
                        {
                            if (len >= 5 && tsPosition == 4)//要素数5以上、タイムスタンプ位置4
                            {
                                var ninusi = fNameArray[2];
                                if (ninusi != "" && ninusi != "登録名称不明" && ninusi != "scan" && ninusi != "取込" && ninusi != "本社受信")
                                {                                    
                                    var sdate = DateTime.Now.ToString("yyyyMMdd");
                                    var edate = DateTime.Now.ToString("yyyyMMdd");
                                    var comment = fNameArray[1];
                                    var commentArray = comment.Split('-');
                                    var datestr = commentArray[0];
                                    if (DateTime.TryParseExact(datestr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                                    {
                                        sdate = datestr;
                                        edate = datestr;
                                        if (commentArray.Length > 1)
                                        {
                                            comment = commentArray[1];
                                            if (commentArray.Length > 2)
                                            {
                                                for (int i = 2; i < commentArray.Length; i++)
                                                {
                                                    comment = comment + "-" + commentArray[i];
                                                }
                                            }
                                        }
                                    }
                                    var faxnumber = fNameArray[3];
                                    if (faxnumber == "" && index.FAX番号 != "")
                                    {
                                        faxnumber = index.FAX番号;
                                    }
                                    fileNameElement.荷主名 = ninusi;
                                    fileNameElement.FAX番号 = faxnumber;
                                    fileNameElement.タイムスタンプ = fNameArray[4][..14];
                                    fileNameElement.受注CD = fNameArray[0];
                                    fileNameElement.開始日 = sdate;
                                    fileNameElement.終了日 = edate;
                                    fileNameElement.担当部署 = index.担当部署;
                                    fileNameElement.備考 = comment;
                                    fileNameElement.ページ = "";
                                    fileTransferHistory.ConsignorID = index.荷主ID;
                                    fileTransferHistory.ConsignorName = ninusi;
                                    fileTransferHistory.FaxNumber = faxnumber;
                                    fileTransferHistory.TimeStamp = fNameArray[4][..14];
                                    fileTransferHistory.StartDate = DateTime.ParseExact(sdate, "yyyyMMdd", null);
                                    fileTransferHistory.EndDate = DateTime.ParseExact(edate, "yyyyMMdd", null);
                                    fileTransferHistory.Department = index.担当部署;
                                    fileTransferHistory.Remarks = comment;
                                    

                                    if (DateTime.TryParseExact(edate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime defaultDate))
                                    {
                                        fileTransferHistory.TemporaryStorageLimit = defaultDate.AddDays(30);
                                    }
                                }
                                else
                                {
                                    emsg = "仕分先不明";
                                }
                            }
                            else//スキャンされたファイル等、整頓FAXでファイル名が生成されていないファイル、
                            {
                                emsg = "ファイル名構成不正";
                            }
                        }
                        else//通常受注ファイル
                        {
                            if (len >= 4 && tsPosition == 3)
                            {
                                var ninusi = fNameArray[1];
                                if (ninusi != "登録名称不明" && ninusi != "scan" && ninusi != "取込" && ninusi != "")
                                {
                                    if (index.名称_TF == "")
                                    {
                                        await this.UpdateNinusimeiFromDB(index.荷主ID, fNameArray[1]);
                                    }
                                }
                                var faxnumber = fNameArray[2];
                                if (faxnumber == "")
                                {
                                    if (index.FAX番号 != "")
                                    {
                                        faxnumber = index.FAX番号;
                                    }
                                }
                                else
                                {
                                    if (index.FAX番号 == "")
                                    {
                                        await this.UpdateFaxNumberFromDB(index.荷主ID, faxnumber);
                                    }
                                }
                                var page = "";
                                //if (len - 1 > tsPosition)
                                //{
                                //    for (var i = tsPosition + 1; i < len; i++)
                                //    {
                                //        page += fNameArray[i];
                                //    }
                                //}
                                fileNameElement.荷主名 = index.荷主名;
                                fileNameElement.FAX番号 = faxnumber;
                                fileNameElement.タイムスタンプ = fNameArray[3][..14];
                                fileNameElement.受注CD = fNameArray[0];
                                fileNameElement.開始日 = index.開始日;
                                fileNameElement.終了日 = index.終了日;
                                fileNameElement.担当部署 = index.担当部署;
                                fileNameElement.備考 = index.備考;
                                fileNameElement.ページ = page;
                                fileTransferHistory.ConsignorID = index.荷主ID;
                                fileTransferHistory.ConsignorName = ninusi;
                                fileTransferHistory.FaxNumber = faxnumber;
                                fileTransferHistory.TimeStamp = fNameArray[3][..14];
                                fileTransferHistory.StartDate = DateTime.ParseExact(index.開始日, "yyyyMMdd", null);
                                fileTransferHistory.EndDate = DateTime.ParseExact(index.終了日, "yyyyMMdd", null);
                                fileTransferHistory.Department = index.担当部署;
                                fileTransferHistory.Remarks = index.備考;

                                if (DateTime.TryParseExact(index.終了日, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime defaultDate))
                                {
                                    fileTransferHistory.TemporaryStorageLimit = await GetTemporaryStorageLimit(defaultDate, index.請求締日);
                                }

                            }
                            else
                            {
                                emsg = "ファイル名構成不正";
                            }
                        }
                    }
                    else
                    {
                        emsg = "受注データなし";
                    }
                }
                else
                {
                    emsg = "受注コード不正";
                }
            }
            catch
            {
                emsg = "例外発生";
            }

            if (emsg == "")
            {
                //正常転送処理
                await this.MoveToSuccessDirectory(fileNameElement, fileTransferHistory, fileTransferLog, cubeState);
            }
            else
            {
                //エラー戻し転送
                await this.MoveToErrorDirectory(fullPath, emsg, fileTransferLog, cubeState);
            }
        }


        private async Task MoveToSuccessDirectory(FileNameElement fileNameElement, FileTransferHistory fileTransferHistory, FileTransferLog fileTransferLog, bool cubeState)
        {
            var setting = await _settingService.GetSetting(_serialNumber);//           
            var rootDirectory = setting.Trans2Successful;  
            var directoryPath = fileNameElement.荷主名 + @"\" + fileNameElement.担当部署;
            if (fileNameElement.担当部署 == "4.倉庫保管")
            {
                directoryPath += @"\" + fileNameElement.備考;
            }
            var directoryTo = rootDirectory + @"\" + directoryPath;
            if (!Directory.Exists(directoryTo))
            {
                Directory.CreateDirectory(directoryTo);
            }
            var newFileName = fileNameElement.荷主名 + "_" + 
                                fileNameElement.FAX番号 + "_" + 
                                fileNameElement.タイムスタンプ + "_" +
                                fileNameElement.受注CD + "_" + 
                                fileNameElement.開始日 + "_" + 
                                fileNameElement.終了日 + "_" +
                                fileNameElement.担当部署 + "_" +
                                fileNameElement.備考 + "_" +
                                fileNameElement.ページ;
            var newPath = directoryTo + @"\" + newFileName + fileNameElement.拡張子;
            int i = 1;
            while (File.Exists(newPath))
            {
                newPath = directoryTo + @"\" + newFileName + "(" + i.ToString() + ")" + fileNameElement.拡張子;
                i++;
            }

            //Tf
            var newTfPath = setting.TfUploader + @"\" + newFileName + fileNameElement.拡張子;
            i = 1;
            while (File.Exists(newTfPath))
            {
                newTfPath = setting.TfUploader + @"\" + newFileName + "(" + i.ToString() + ")" + fileNameElement.拡張子;
                i++;
            }

            //DocumentIndex
            var diDrectory = @"C:\FileStrage\" + fileNameElement.荷主名 + @"\" + fileNameElement.担当部署;
            if (fileNameElement.担当部署 == "4.倉庫保管")
            {
                diDrectory += @"\" + fileNameElement.備考;
            }
            if (fileNameElement.開始日.Length == 8)
            {
                diDrectory += string.Concat(@"\", fileNameElement.開始日.AsSpan(0, 6));
            }
            if (!Directory.Exists(diDrectory))
            {
                Directory.CreateDirectory(diDrectory);
            }
            var diPath = diDrectory + @"\" + newFileName + fileNameElement.拡張子;
            i = 1;
            while (File.Exists(diPath))
            {
                diPath = diDrectory + @"\" + newFileName + "(" + i.ToString() + ")" + fileNameElement.拡張子;
                i++;
            }


            try
            {
                var dt = DateTime.Now;
                var fileInfo = new FileInfo(fileNameElement.元ファイルFullPath);
                
                fileInfo.CopyTo(diPath);//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
                //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
                fileInfo.CopyTo(newTfPath);//===========================================================Therefore用転送
                //######################################################################################本保存
                fileInfo.MoveTo(newPath);//#############################################################
                //######################################################################################
                
                fileTransferLog.Result = "正常";
                fileTransferLog.ProcessedDateTime = dt;
                //======================================================================================Therefore用転送
                fileTransferLog.Process = "Therefore";//================================================
                fileTransferLog.FullPathAfterTransfer = newTfPath;//====================================
                await _fileTransferLogService.InsertFileTranceferLogAsync(fileTransferLog);//===========
                if(cubeState)//=========================================================================
                {//=====================================================================================
                    await _thereforeLogService.AddThereforeLogAsync(fileTransferLog);//=================
                }//=====================================================================================
                //======================================================================================
                
                fileTransferLog.Process = "一時保管";//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%FileTransferLog
                fileTransferLog.FullPathAfterTransfer = newPath;//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                await _fileTransferLogService.InsertFileTranceferLogAsync(fileTransferLog);//%%%%%%%%%%%
                fileTransferHistory.TemporaryStorageFullPath = newPath;//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                fileTransferHistory.TemporaryStorageTime = dt;//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                await _fileTransferHistoryService.InsertFileTransferHistoryAsync(fileTransferHistory);//

                var fileRegistry = new FileRegistry//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&FileRegistry
                {//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    FileFullPath = newPath,//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    Thumbnail = this.GeneratePdfThumbnail(newPath),//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    CreatedDate = dt,//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    ZyutyuuID = Convert.ToDecimal(fileNameElement.受注CD),//&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    ConsignorName = fileNameElement.荷主名,//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    Remarks = fileNameElement.備考,//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    Department = fileNameElement.担当部署,//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    RootDirectory = rootDirectory,//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    DirectoryPath = directoryPath,//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    FileName = Path.GetFileName(newPath)//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                };//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                if (DateTime.TryParseExact(fileNameElement.開始日, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sdt))
                {//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    fileRegistry.StartDate = sdt;//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                }//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                if (DateTime.TryParseExact(fileNameElement.終了日, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edt))
                {//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    fileRegistry.EndDate = edt;//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                }//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                await _fileRegistryService.InsertFileRegistryAsync(fileRegistry);//&&&&&&&&&&&&&&&&&&&&&
                fileTransferLog.Process = "Registry";//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                fileTransferLog.FullPathAfterTransfer = newPath;//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                await _fileTransferLogService.InsertFileTranceferLogAsync(fileTransferLog);//&&&&&&&&&&&

                //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
                var info = new FileInfo(diPath);
                var documentIndex = new DocumentIndex
                {
                    FileName = Path.GetFileName(diPath),
                    FilePath = diPath,
                    FileSize = info.Length,
                    CreatedDate = dt,
                    ModifiedDate = dt,
                    FileType = Path.GetExtension(diPath),
                    IsDeleted = false,
                    Checksum = ComputeFileHash(diPath),
                    ThumbnailPath = GenerateThumbnail(diPath),
                    OrderId = Convert.ToDecimal(fileNameElement.受注CD),
                    ClientName = fileNameElement.荷主名,
                    Department = fileNameElement.担当部署,
                    Remarks = fileNameElement.備考,
                    StartDate = sdt,
                    EndDate = edt,
                    OrderAmount = 0,
                };
                await _documentIndexService.InsertDocumentIndexAsync(documentIndex);   
                //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

            }
            catch (Exception e)
            {
                Console.WriteLine(e);                
            }



        }
        private async Task MoveToErrorDirectory(string fullPath, string emsg, FileTransferLog fileTransferLog, bool cubeState)
        {
            var setting = await _settingService.GetSetting(_serialNumber);//
            var fileName = Path.GetFileNameWithoutExtension(fullPath);
            var extensionName = Path.GetExtension(fullPath);
            //this.txtAPPログ.Text += "\r\n" + file;
            //this.txtAPPログ.Text += "\r\n" + fullPath;
            //this.txtAPPログ.Text += "\r\n" + directoryName+ @"\" + fileName + extensionName;

            var fNameArray = fileName.Split('_');
            var newFileName = "";
            //var zyutyuuID = Convert.ToInt32(fNameArray[0]);
            var sp = 1;
            //if (zyutyuuID == 501 || zyutyuuID == 601 || zyutyuuID == 701 || zyutyuuID == 801 || zyutyuuID == 901)
            //{
            //    sp = 2;
            //}
            for (var n = sp; n < fNameArray.Length; n++)
            {
                if (n > sp)
                {
                    newFileName += "_";
                }
                newFileName += fNameArray[n];
            }
            var newPath = setting.Trans2Error + @"\" + emsg + newFileName + extensionName;
            int i = 1;
            while (File.Exists(newPath))
            {
                newPath = setting.Trans2Error + @"\" + emsg + newFileName + "(" + i.ToString() + ")" + extensionName;
            }
            try
            {
                var fileInfo = new FileInfo(fullPath);
                fileInfo.MoveTo(newPath);
                fileTransferLog.Result = emsg;
                fileTransferLog.ProcessedDateTime = DateTime.Now;
                await _fileTransferLogService.InsertFileTranceferLogAsync(fileTransferLog);
                //======================================================================================Therefore用転送
                if (cubeState)//========================================================================
                {//=====================================================================================
                    await _thereforeLogService.AddThereforeLogAsync(fileTransferLog);//=================
                }//=====================================================================================
                //======================================================================================
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private async Task UpdateNinusimeiFromDB(int ninusiID, string ninusimei)
        {
            await _ninusiInfoService.UpdateNinusimeiAsync(ninusiID, ninusimei);
        }
        private async Task UpdateFaxNumberFromDB(int ninusiID, string faxnumber)
        {
            await _ninusiInfoService.UpdateFaxNumberAsync(ninusiID, faxnumber);
        }


        private async Task<DateTime> GetTemporaryStorageLimit(DateTime defaultDate, int cutOfDate)
        {
            var setting = await _settingService.GetSetting(_serialNumber);
            var transferQueueTime = setting.TransferQueueTime;
            if (cutOfDate == 31 || cutOfDate == 0)//末締め
            {
                defaultDate = defaultDate.AddMonths(1);
                defaultDate = new DateTime(defaultDate.Year, defaultDate.Month, 1, 0, 0, 0);
                defaultDate = defaultDate.AddDays(-1); 
            }
            else//末締め以外
            {
                if (defaultDate.Day > cutOfDate)//締日過ぎ
                {
                    defaultDate = defaultDate.AddMonths(1);                    
                }
                defaultDate = new DateTime(defaultDate.Year, defaultDate.Month, cutOfDate , 0, 0, 0);
            }            
            return defaultDate.AddDays(transferQueueTime);
        }

        private byte[] GeneratePdfThumbnail(string pdfFilePath)
        {
            if (!File.Exists(pdfFilePath))
            {
                throw new FileNotFoundException($"The file does not exist: {pdfFilePath}");
            }

            try
            {
                using (var pdfDocument = PdfiumViewer.PdfDocument.Load(pdfFilePath))
                {
                    // PDFページ数を確認
                    if (pdfDocument.PageCount == 0)
                    {
                        throw new InvalidOperationException("The PDF file contains no pages.");
                    }

                    // 指定したページを画像として描画 (ここでは最初のページ 0)
                    using (var image = pdfDocument.Render(0, 256, 256, dpiX: 72, dpiY: 72, false))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            // サムネイルをJPEG形式でメモリに保存
                            image.Save(memoryStream, ImageFormat.Jpeg);
                            return memoryStream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーの詳細をログまたは例外として出力
                Console.WriteLine($"An error occurred while generating the PDF thumbnail: {ex.Message}");
                throw;
            }
        }


        static readonly HashAlgorithm hashProvider = new SHA1CryptoServiceProvider();
        public static string ComputeFileHash(string filePath)
        {
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var bs = hashProvider.ComputeHash(fs);
            return BitConverter.ToString(bs).ToLower().Replace("-", "");
        }

        public string GenerateThumbnail(string pdfPath)
        {
            var thumbnailFolder = @"C:\FileStrage\Thumbnail";
            if (!File.Exists(pdfPath))
            {
                throw new FileNotFoundException("PDFファイルが見つかりません", pdfPath);
            }

            string fileName = Path.GetFileNameWithoutExtension(pdfPath) + ".jpg";
            string thumbnailPath = Path.Combine(thumbnailFolder, fileName);

            using (var document = PdfDocument.Load(pdfPath))
            using (var image = document.Render(0, 256, 256, true))
            {
                image.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            return thumbnailPath;
        }









    }
}
