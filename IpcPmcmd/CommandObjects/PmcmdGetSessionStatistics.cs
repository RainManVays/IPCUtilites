namespace IPCUtilities.IpcPmcmd
{
    public class PmcmdGetSessionStatistics:AbstractTaskParams
    {
        private string _workflow;
        private string _workflowTaskInstancePath;
        public string Workflow { get { return _workflow; } set { _workflow = " -workflow " + value; } }
        public string WorkflowTaskInstancePath { get { return _workflowTaskInstancePath; } set { _workflowTaskInstancePath = " " + value; } }

    }
}
