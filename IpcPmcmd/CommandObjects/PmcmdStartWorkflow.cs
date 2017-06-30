namespace IPCUtilities.IpcPmcmd
{
   public class PmcmdStartWorkflow:AbstractFolderRunParam
    {
        private string _startFrom;
        private string _paramFile;
        private string _localParamFile;
        private string _osprofile;
        private string _workflow;

        public string StartFrom { get { return _startFrom; } set { _startFrom = " -startfrom " + value; } }
        public bool Wait { get; set; }
        public bool Recovery { get; set; }
        public string ParamFile { get { return _paramFile; } set { _paramFile = " -paramfile " + value; } }
        public string LocalParamFile { get { return _localParamFile; } set { _localParamFile = " -lpf " + value; } }
        public string Osprofile { get { return _osprofile; } set { _osprofile = " -o " + value; } }
        public string Workflow { get { return _workflow; } set { _workflow = " -workflow " + value; } }
    }
}
