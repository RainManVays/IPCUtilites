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
        private string _controlFileEncoding;
        private string _logFile;

        public string ImportXml { get { return _importXml; } set { _importXml = " -i " + value; } }
        public string SourceFolder { get { return _sourceFolder; } set { _sourceFolder =  value; } }
        public string SourceRepo { get { return _sourceRepo; } set { _sourceRepo = value; } }
        public string TargetFolder { get { return _targetFolder; } set { _targetFolder = value; } }
        public string TargetRepo { get { return _targetRepo; } set { _targetRepo =  value; } }
        public string ControlFileEncoding { get { return _controlFileEncoding; } set { _controlFileEncoding = value; } }
        public string ImportDtdFile { get { return _importDtdFile; } set { _importDtdFile = value; } }
        public string LogFile { get { return _logFile; } set { _logFile = " -l " + value; } }
    }
}