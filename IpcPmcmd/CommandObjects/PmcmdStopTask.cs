namespace IPCUtilities.IpcPmcmd.CommandObjects
{
    public class PmcmdStopTask:AbstractTaskParams
    {
        private string _workflow;
        private string _taskInstancePath;
        public bool Wait { get; set; }
        public string Workflow { get { return _workflow; } set { _workflow = " -workflow " + value; } }
        public string TaskInstancePath { get { return _taskInstancePath; } set { _taskInstancePath = " " + value; } }
    }
}
