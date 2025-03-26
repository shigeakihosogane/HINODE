using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public class FolderNode
    {
        public string Id { get; set; } = ""; // フォルダのユニークID
        public string Name { get; set; } = ""; // フォルダ名
        public string Icon { get; set; } = "";        
        public string FullPath { get; set; } = ""; // フォルダのパス
        public bool HasSubfolders { get; set; } = false;// サブフォルダが存在するかどうか
        public List<FolderNode> SubFolders { get; set; } = new List<FolderNode>(); // サブフォルダのリスト
        public bool Expanded { get; set; } = false;   // 展開状態（初期は閉じた状態）
    }
}
