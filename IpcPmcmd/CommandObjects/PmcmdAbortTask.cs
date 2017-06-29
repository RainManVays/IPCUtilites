namespace IPCUtilities.IpcPmcmd
{
    public class PmcmdAbortTask:AbstractTaskParams
    {
        private string _workflow;
        private string _taskInstancePath;
        public string Workflow { get { return _workflow; } set { _workflow = " -workflow " + value; } }
        public bool Wait { get; set; }
        public string TaskInstancePath { get { return _taskInstancePath; } set { _taskInstancePath = " " + value; } }
    }
}
