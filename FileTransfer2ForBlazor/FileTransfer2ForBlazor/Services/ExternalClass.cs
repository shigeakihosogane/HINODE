using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Services
{
    public class ExternalClass
    {
        private readonly SharedService _sharedService;

        public ExternalClass(SharedService sharedService)
        {
            _sharedService = sharedService;
        }

        public async Task CallGetFileTranceferLogAsync()
        {
            await _sharedService.TriggerInsertFileTranceferLogAsync();
        }

        public async Task CallGetFileTransferHistoryAsync()
        {
            await _sharedService.TriggerInsertFileTransferHistoryAsync();
        }
        public async Task CallGetFileRegistryAsync()
        {
            await _sharedService.TriggerInsertFileRegistryAsync();
        }






    }
}
