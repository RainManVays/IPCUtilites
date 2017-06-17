namespace IPCUtilities.IpcPmcmd.CommandObjects
{
    class WorkfllowsDetails
    {
        string Folder { get; set; }
        string WorkflowName { get; set;}
        int Version { get; set; }
        string RunStatus { get; set; }
        int FirstErrorCode { get; set; }
        string StartTime { get; set; }
        string LogFile { get; set; }

        string RunType { get; set; }
        string RunUser { get; set; }
        TaskDetails Tasks { get; set; }
    }
}
