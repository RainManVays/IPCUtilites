namespace IPCUtilities.IpcPmcmd.CommandObjects
{
    public class PmcmdGetTaskDetails:AbstractFolderRunParam
    {
        private string _workflow;
        private string _taskInstancePath;
        public string Workflow { get { return _workflow; } set { _workflow = " -workflow " + value; } }
        public string TaskInstancePath { get { return _taskInstancePath; } set { _taskInstancePath = " " + value; } }
    }
}
