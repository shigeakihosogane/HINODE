using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Models
{
    public class TranceferLog
    {
        public TranceferLog(int id,string syori,string kekka,string mae,string ato,DateTime dt)
        {
            ID = id;
            処理 = syori;
            結果 = kekka;
            変更前Fullpath = mae;
            変更後Fullpath = ato;
            日時 = dt;
        }

        public int ID { get; set; }
        public string 処理 { get; set; } = "";
        public string 結果 { get; set; } = "";
        public string 変更前Fullpath { get; set; } = "";
        public string 変更後Fullpath { get; set; } = "";
        public DateTime 日時 { get; set; }

    }



    



}
