using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchSystem.Models
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




    }
}
