using System;

namespace IPCUtilities.IpcPmcmd
{
    public class Transformation
    {
        public string Partition { get; set; }
        public string TransformInstance { get; set; }
        public string Name { get; set; }
        public Int64 AppliedRows { get; set; }
        public Int64 AffectedRows { get; set; }
        public Int64 RejectedRows { get; set; }
        public Int64 ThroughputRows { get; set; }
        public Int64 ThroughputBytes { get; set; }
        public int LastErrorCode { get; set; }
        public string LastErrorMessage { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
