using System;

namespace IPCUtilities.IpcPmcmd
{
   public class TaskDetails:IntegrationService
    {
        public IntegrationService Service { get; set; }
        public string Folder { get; set; }
        public string WorkflowName { get; set; }
        public int WorkflowVersion { get; set; }
        public string SessionName { get; set; }
        public int SessionVersion { get; set; }
        public string TaskType { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RunStatus { get; set; }
        public int ErrorCode { get; set; }
        public string ServiceProcess { get; set; }
        public string ServiceGrid{ get; set; }
        public string RunMode { get; set; }
        public string MappingName { get; set; }
        public string LogFile { get; set; }
        public int FirstErrorCode { get; set; }
        public string FirstErrorMessage { get; set; }
        public Int64 SourceSuccesRows { get; set; }
        public Int64 SourceFailedRows { get; set; }
        public Int64 TargetSuccesRows { get; set; }
        public Int64 TargetFailedRows { get; set; }
        public int NumberTransformError { get; set; }
    }
}
