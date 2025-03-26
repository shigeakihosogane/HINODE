using FileBrowser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Services
{

    public class RootDirectoryService
    {

        public RootDirectoryService()
        { 
        }


        public async Task<List<RootDirectory>> GetRootDirectoryAsync()
        {
            var rootDirectorys = new List<RootDirectory>();
            await Task.Run(() =>
            {
                rootDirectorys.Add(new RootDirectory
                {
                    Sort = 1,
                    DisplayName = "受信FAX保管",
                    DirectoryPath = @"\\192.168.2.240\受注FAX保管"
                });

                rootDirectorys.Add(new RootDirectory
                {
                    Sort = 2,
                    DisplayName = "FAX一時保管TEST",
                    DirectoryPath = @"C:\Users\Hinode24\Documents\Test\Fax受信\FAX一時保管"
                });

            });
            return rootDirectorys;
        }










    }
}
