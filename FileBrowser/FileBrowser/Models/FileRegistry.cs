using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public class FileRegistry
    {
        public int Id { get; set; } = 0;
        public string FileFullPath { get; set; } = ""; //ファイルの保存先（フルパス）        
        public byte[]? Thumbnail { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal ZyutyuuID { get; set; } = 0;
        public string ConsignorName { get; set; } = "";
        public string Department { get; set; } = "";
        public string Remarks { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal OrderAmount { get; set; } = 0;
        public bool IsExecutionCompleted { get; set; } = false;
        public string RootDirectory { get; set; } = "";
        public string DirectoryPath { get; set; } = "";
        public string FileName { get; set; } = "";
    }
}
