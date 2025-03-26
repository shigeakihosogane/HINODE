using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Models
{
    public class DailyTaskResult
    {
        public DateTime ExecutionTime { get; set; }
        public int TotalSuccesses { get; set; } = 0;
        public int TotalFailures { get; set; } = 0;
        public int TotalTemporaryStorage {  get; set; } = 0;
        public string Comment { get; set; } = "";

    }
}
