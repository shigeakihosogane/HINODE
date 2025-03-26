

using System;

namespace WindowsFormsApp2.Models
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

    }

    public class ArchiveInfo
    {
        public decimal 受注ID { get; set; } = 0;        
        public decimal Amount { get; set; }=0;
        public bool ArchiveFlag { get; set; }=false;
        
    }

    




}




