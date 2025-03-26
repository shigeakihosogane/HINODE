using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Models
{
    public class DocumentIndex
    {
        public Int64 Id { get; set; } = 0;
        public string FileName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public Int64 FileSize { get; set; } = 0;
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string FileType { get; set; } = "";
        public bool IsDeleted { get; set; } = false;

        public string Checksum { get; set; } = "";
        public string ThumbnailPath { get; set; } = "";

        public decimal OrderId { get; set; } = 0;
        public string ClientName { get; set; } = "";
        public string Department { get; set; } = "";
        public string Remarks { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal OrderAmount { get; set; } = 0;

    }
}
