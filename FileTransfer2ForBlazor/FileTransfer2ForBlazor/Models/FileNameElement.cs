using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Models
{
    public class FileNameElement
    {
        public string 荷主名 { get; set; } = "";
        public string FAX番号 { get; set; } = "";
        public string タイムスタンプ { get; set; } = "";
        public string 受注CD { get; set; } = "";
        public string 開始日 { get; set; } = "";
        public string 終了日 { get; set; } = "";
        public string 担当部署 { get; set; } = "";
        public string 備考 { get; set; } = "";
        public string ページ { get; set; } = "";
        public string 拡張子 { get; set; } = "";
        public string 元ファイルFullPath { get; set; } = "";
    }
}
