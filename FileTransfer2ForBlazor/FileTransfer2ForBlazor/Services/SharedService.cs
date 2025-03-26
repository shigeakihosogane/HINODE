

namespace FileTransfer2ForBlazor.Services
{
    public class SharedService
    {
        public event Func<Task>? OnInsertFileTranceferLog;
        public event Func<Task>? OnInsertFileTransferHistory;
        public event Func<Task>? OnInsertFileRegistry;


        public async Task TriggerInsertFileTranceferLogAsync()
        {
            if (OnInsertFileTranceferLog != null)
            {
                foreach (var handler in OnInsertFileTranceferLog.GetInvocationList())
                {
                    if (handler is Func<Task> asyncHandler)
                    {
                        await asyncHandler();
                    }
                }
            }
        }

        public async Task TriggerInsertFileTransferHistoryAsync()
        {
            if (OnInsertFileTransferHistory != null)
            {
                foreach (var handler in OnInsertFileTransferHistory.GetInvocationList())
                {
                    if (handler is Func<Task> asyncHandler)
                    {
                        await asyncHandler();
                    }
                }
            }
        }
        public async Task TriggerInsertFileRegistryAsync()
        {
            if (OnInsertFileRegistry != null)
            {
                foreach (var handler in OnInsertFileRegistry.GetInvocationList())
                {
                    if (handler is Func<Task> asyncHandler)
                    {
                        await asyncHandler();
                    }
                }
            }
        }

    }
}
