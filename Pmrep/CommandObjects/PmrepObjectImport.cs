namespace IPCUtilities.IpcPmrep
{
    public class PmrepObjectImport
    {
        private string _importXml;
        private string _sourceFolder;
        private string _sourceRepo;
        private string _targetFolder;
        private string _targetRepo;
        private string _importDtdFile;
        private string _importControlFile;
        private string _logFile;

        public string importXml { get { return _importXml; } set { _importXml = " -i " + value; } }
        public string sourceFolder { get { return _sourceFolder; } set { _sourceFolder =  value; } }
        public string sourceRepo { get { return _sourceRepo; } set { _sourceRepo = value; } }
        public string targetFolder { get { return _targetFolder; } set { _targetFolder = value; } }
        public string targetRepo { get { return _targetRepo; } set { _targetRepo =  value; } }
        public string importControlFile { get { return _importControlFile; } set { _importControlFile = " -c " + value; } }
        public string importDtdFile { get { return _importDtdFile; } set { _importDtdFile = value; } }
        public string logFile { get { return _logFile; } set { _logFile = " -l " + value; } }
    }
}