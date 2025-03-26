using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public class SearchCondition
    {
        public decimal? ZyutyuuID { get; set; } = null;
        public string ConsignorName { get; set; } = "";
        public string Department { get; set; } = "";
        public string Remarks { get; set; } = "";
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public decimal? OrderAmountUpper { get; set; } = null;
        public decimal? OrderAmountLower { get; set; } = null;
    }
}
