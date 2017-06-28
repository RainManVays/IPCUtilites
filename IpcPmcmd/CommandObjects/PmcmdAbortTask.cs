namespace IPCUtilities.IpcPmcmd
{
    class PmcmdAbortTask:AbstractTaskParams
    {
        private string _workflow;
        public virtual string Workflow { get { return _workflow; } set { _workflow = " -workflow " + value; } }
        public bool Wait { get; set; }
    }
}
