using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Models
{
    public class ImageInfo
    {
        public string Id { get; set; } = "";         
        public DateTime? CreateDate { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageName { get; set; } = "";
        public bool IdDelete {  get; set; }=false;
        public decimal 受注ID { get; set; } = 0;
        public int 荷主ID { get; set; } = 0;
        public string 荷主名 { get; set; } = "";
        public string 担当部署 { get; set; } = "";
        public string 備考 { get; set; } = "";
        public DateTime? 開始日 { get; set; }
        public DateTime? 終了日 { get; set; }



    }
}
