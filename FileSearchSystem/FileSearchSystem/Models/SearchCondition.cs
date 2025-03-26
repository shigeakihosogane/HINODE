using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchSystem.Models
{
    public class SearchCondition
    {
        public decimal? ZyutyuuID { get; set; } = null;
        public string ConsignorName { get; set; } = "";
        public string Department { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
        public decimal? OrderAmountUpper { get; set; } = null;
        public decimal? OrderAmountLower { get; set; } = null;

    }
}
