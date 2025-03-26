using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public class DisplayContent
    {        
        public string Name { get; set; } = "";
        public bool IsFolder { get; set; } = false;
        public string FullPath { get; set; } = "";
        public string NetworkPath { get; set; } = "";

    }
}
