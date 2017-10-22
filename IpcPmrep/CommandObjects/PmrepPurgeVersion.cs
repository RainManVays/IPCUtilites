namespace IPCUtilities.IpcPmrep.CommandObjects
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

        public string Version { get { return _version; } set { _version = " -d " + value; } }
        public string LastNVersionsToKeep { get { return _lastNVersionsToKeep; } set { _lastNVersionsToKeep = " -n " + value; } }
        public string TimeDate { get { return _timeDate; } set { _timeDate = " -t " + value; } }
        public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string QueryName { get { return _queryName; } set { _queryName = " -q " + value; } }
        public string OutputFileName { get { return _outputFileName; } set { _outputFileName = " -o " + value; } }
        public string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
        public bool PrewPurgedObjOnly { get; set; }
        public bool Verbose { get; set; }
        public bool CheckDeplGroupReference { get; set; }
        public bool LogObjNotPurged { get; set; }
    }
}