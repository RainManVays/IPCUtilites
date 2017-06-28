namespace IPCUtilities.IpcPmcmd
{
    class PmcmdStartWorkflow
    {
        private string _folder;
        private string _startFrom;
        private string _recovery;
        private string _paramFile;
        private string _localParamFile;
        private string _osprofile;
        private string _runinsname;
        public string Folder { get { return _folder; } set { _folder = " -folder " + value; } }
        public string StartFrom { get { return _startFrom; } set { _startFrom = " -startfrom " + value; } }
        public string Recovery { get { return _recovery; } set { _recovery = " -recovery " + value; } }
        public string ParamFile { get { return _paramFile; } set { _paramFile = " -paramfile " + value; } }
        public string LocalParamFile { get { return _localParamFile; } set { _localParamFile = " -lpf " + value; } }
        public string Osprofile { get { return _osprofile; } set { _osprofile = " -o " + value; } }
        public string Runinsname { get { return _runinsname; } set { _runinsname = " -rin " + value; } }
    }
}
