namespace IPCUtilities.IpcPmcmd
{
    public class PmcmdAbortWorkflow: AbstractTaskParams
    {
        private string _workflow;
        public bool Wait { get; set; }
        public string Workflow { get { return _workflow; } set { _workflow = " " + value; } }
    }
}
