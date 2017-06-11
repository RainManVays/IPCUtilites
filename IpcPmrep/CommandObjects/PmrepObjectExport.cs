namespace IPCUtilities.IpcPmrep
{
    public class PmrepObjectExport:AbstractRepoObject
    {
        private string _versionNumber;
        private string _folderName;
        private string _persistentInputFile;
        private string _logFileName;
        private string _dbdSeparator;
        private string _xnlOutputFileName;

        public string versionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string persistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
        public string logFileName { get { return _logFileName; } set { _logFileName = " -l " + value; } }
        public string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -e " + value; } }

        public string xnlOutputFileName { get { return _xnlOutputFileName; } set { _xnlOutputFileName = " -u " + value; } }


    }
}