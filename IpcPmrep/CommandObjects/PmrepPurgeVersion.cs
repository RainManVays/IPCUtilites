namespace IPCUtilities.IpcPmrep
{
    public class PmrepPurgeVersion
    {
        private string _version;
        private string _lastNVersionsToKeep;
        private string _timeDate;
        private string _folderName;
        private string _queryName;
        private string _outputFileName;
        private string _dbdSeparator;

        public string version { get { return _version; } set { _version = " -d " + value; } }
        public string lastNVersionsToKeep { get { return _lastNVersionsToKeep; } set { _lastNVersionsToKeep = " -n " + value; } }
        public string timeDate { get { return _timeDate; } set { _timeDate = " -t " + value; } }
        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string queryName { get { return _queryName; } set { _queryName = " -q " + value; } }
        public string outputFileName { get { return _outputFileName; } set { _outputFileName = " -o " + value; } }
        public string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
        public bool prewPurgedObjOnly { get; set; }
        public bool verbose { get; set; }
        public bool checkDeplGroupReference { get; set; }
        public bool logObjNotPurged { get; set; }
    }
}