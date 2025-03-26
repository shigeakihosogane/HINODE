

namespace FileTransfer2ForBlazor.Models
{
    public class Setting
    {
        public string Trans1Monitoring { get; set; } = "";
        public string Trans1Successful { get; set; } = "";
        public string Trans2Monitoring { get; set; } = "";
        public string Trans2Successful { get; set; } = "";
        public string Trans2Error { get; set; } = "";

        public string TfUploader { get; set; } = "";
        public string ArchiveFolder { get; set; } = "";
        public string FileListFolder { get; set; } = "";
        public int TransferQueueTime { get; set; } = 0;        
        public int MonitoringInterval { get; set; } = 0;
        public DateTime ScheduledExecutionTime {  get; set; } = DateTime.MinValue;
        public int LogRetrievalCount { get; set; } = 0; 

    }
}
