namespace IPCUtilities.IpcPmcmd
{
    public class WorkflowDetails
    {
        public IntegrationService Service { get; set; }
        public string Folder { get; set; }
        public string WorkflowName { get; set; }
        public int Version { get; set; }
        public string RunStatus { get; set; }
        public int ErrorCode { get; set; }
        public int WorkflowRunId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LogFile { get; set; }
        public string RunType { get; set; }
        public string RunUser { get; set; }
        public string OsProfile { get; set; }
        
    }
}
