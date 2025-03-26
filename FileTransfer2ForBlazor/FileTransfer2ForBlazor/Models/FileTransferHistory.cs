using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Models
{
    public class FileTransferHistory
    {
        public int Id { get; set; } = 0;
        public decimal ZyutyuuID { get; set; } = 0;
        public string DefaultFullPath { get; set; } = "";
        public string TemporaryStorageFullPath { get; set; } = "";
        public DateTime? TemporaryStorageTime { get; set; }
        public DateTime? TemporaryStorageLimit { get; set; }
        public string ArchiveFullPath { get; set; } = "";
        public DateTime? ArchiveTime { get; set; }
        public decimal OrderAmount { get; set; } = 0;

        //0荷主名_1FAX番号_2タイムスタンプ_3受注ID_4開始日_5終了日_6担当区分_7備考_8金額_9ページ
        public int ConsignorID { get; set; } = 0; // 荷主ID
        public string ConsignorName { get; set; } = ""; // 荷主名
        public string FaxNumber { get; set; } = ""; // FAX番号
        public string TimeStamp { get; set; } = ""; // タイムスタンプ
        public DateTime? StartDate { get; set; } // 開始日
        public DateTime? EndDate { get; set; }// 終了日
        public string Department { get; set; } = ""; // 担当部署
        public string Remarks { get; set; } = ""; // 備考
        
        


    }
}
