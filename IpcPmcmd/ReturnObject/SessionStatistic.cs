using System;
using System.Collections.Generic;

namespace IPCUtilities.IpcPmcmd
{
    public class SessionStatistic
    {
        public string Folder { get; set; }
        public string WorkflowName { get; set; }
        public string SessionName { get; set; }
        public string MappingName { get; set; }
        public string LogFile { get; set; }
        public Int64 SourceSuccesRows { get; set; }
        public Int64 SourceFailedRows { get; set; }
        public Int64 TargetSuccesRows { get; set; }
        public Int64 TargetFailedRows { get; set; }
        public int NumberTransformError { get; set; }
        public int FirstErrorCode { get; set; }
        public string FirstErrorMessage { get; set; }
        public string RunStatus { get; set; }
        public string ServiceName { get; set; }
        public string ServiceProcess { get; set; }
        public string ServiceGrid { get; set; }
        public List<Transformation> Transformations { get; set; }
    }
}
