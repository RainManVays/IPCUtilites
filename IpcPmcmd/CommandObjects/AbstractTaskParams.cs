namespace IPCUtilities.IpcPmcmd
{
    public abstract class  AbstractTaskParams:AbstractFolderRunParam
    {
        private string _workflowRunId;
        
        public virtual string WorkflowRunId { get { return _workflowRunId; } set { _workflowRunId = " -wfrunid " + value; } }
        
    }
}
