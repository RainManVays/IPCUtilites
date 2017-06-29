namespace IPCUtilities.IpcPmcmd
{
    public class PmcmdGetWorkflowDetails: AbstractTaskParams
    {
        private string _workflow;
        public string Workflow { get { return _workflow; } set { _workflow = " " + value; } }
    }
}
