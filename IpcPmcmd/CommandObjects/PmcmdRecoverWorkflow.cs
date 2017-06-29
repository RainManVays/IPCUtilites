namespace IPCUtilities.IpcPmcmd
{
    public class PmcmdRecoverWorkflow
    {
        private string _folder;
        private string _localparamfile;
        private string _paramfile;
        public  string Folder { get { return _folder; } set { _folder = " -folder " + value; } }
        public string ParamFile { get { return _paramfile; } set { _paramfile = " -paramfile " + value; } }
        public  string LocalParamFile { get { return _localparamfile; } set { _localparamfile = " -localparamfile " + value; } }
    }
}
