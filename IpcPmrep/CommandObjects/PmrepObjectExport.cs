namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepObjectExport:AbstractRepoObject
    {
        private string _versionNumber;
        private string _folderName;
        private string _persistentInputFile;
        private string _logFileName;
        private string _dbdSeparator;
        private string _xnlOutputFileName;

        public string VersionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
        public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string PersistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
        public string LogFileName { get { return _logFileName; } set { _logFileName = " -l " + value; } }
        public string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -e " + value; } }

        public string XnlOutputFileName { get { return _xnlOutputFileName; } set { _xnlOutputFileName = " -u " + value; } }


    }
}