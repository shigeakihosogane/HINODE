using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileBrowser
{
    public class DirTreeNode : TreeNode
    {
        public DirTreeNode() { }


        public DirTreeNode(string directoryName,string directoryPath) : base(directoryName)
        {
            this.DirectoryPath = directoryPath;
        }

        public string DirectoryPath { get; set; }

    }
}
