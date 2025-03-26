using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Models
{
    public class FileTransferLog
    {
        public int Id { get; set; }
        public string Process { get; set; } = "";
        public string Result { get; set; } = "";
        public string FullPathBeforeTransfer { get; set; } = "";
        public string FullPathAfterTransfer { get; set; } = "";
        public DateTime? ProcessedDateTime { get; set; }






    }
}
