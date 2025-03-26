using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Models
{
    public class Index
    {
        public int ID { get; set; }
        public decimal 受注ID { get; set; }
        public string 開始日 { get; set; } = "";
        public string 終了日 { get; set; } = "";
        public string 担当部署 { get; set; } = "";
        public string 備考 { get; set; } = "";
        public int 荷主ID { get; set; } = 0;
        public string 荷主名 { get; set; } = "";
        public string 名称_HND { get; set; } = "";
        public string 名称_TF { get; set; } = "";
        public string FAX番号 { get; set; } = "";
        public int 請求締日 { get; set; } = 1;


        
    }
}
