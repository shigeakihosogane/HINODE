using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Models
{
    public class SystemLog
    {
        public SystemLog(DateTime dt,string log) 
        {
            日時 = dt;
            ログ = log;
        }

        public DateTime 日時 {  get; set; }
        public string ログ {  get; set; }
    }
}
