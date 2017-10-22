namespace IPCUtilities.IpcPmcmd.CommandObjects
{
    public class PmcmdStartTask:AbstractFolderRunParam
    {
        private string _paramfile;
        private string _workflow;
        private string _taskInstancePath;
        public string ParamFile { get { return _paramfile; } set { _paramfile = " -paramfile " + value; } }
        public bool Wait { get; set; }
        public bool Recovery { get; set; }
        public string Workflow { get { return _workflow; } set { _workflow = " -workflow " + value; } }
        public string TaskInstancePath { get { return _taskInstancePath; } set { _taskInstancePath = " " + value; } }
    }
}
