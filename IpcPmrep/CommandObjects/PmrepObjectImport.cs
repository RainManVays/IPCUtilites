namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepObjectImport
    {
        private string _importXml;
        private string _logFile;

        public string ImportXml { get { return _importXml; } set { _importXml = " -i " + value; } }
        public string[] SourceFolder { get; set; }
        public string SourceRepo { get; set; }
        public string TargetRepo { get; set; }
        public string ControlFileEncoding { get; set; }
        public string ImportDtdFile { get; set; }
        public string LogFile { get { return _logFile; } set { _logFile = " -l " + value; } }
    }
}