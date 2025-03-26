using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Models
{
    public class FaxdataRegistrationHistory
    {
        public int ID { get; set; }
        public decimal ZyutyuuID { get; set; }
        public DateTime? CreateDateTime {  get; set; }
        public string TempFullPath { get; set; } = "";
        public string ArchiveFullPath { get; set; } = "";
        public DateTime? ArchiveDateTime { get; set; }


    }
}
