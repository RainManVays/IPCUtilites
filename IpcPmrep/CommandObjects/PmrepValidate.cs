namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepValidate
    {
        private string _objectName;
        public ValidateObject ObjectType { get; set; }
        private string _versionNumber;
        private string _folderName;
        private string _persistentInputFile;
        private string _outputOptionTypes;
        private string _persistentOutputFileName;
        private string _endOfRecordSeparator;
        private string _endOfListingIndicator;

        public string ObjectName { get { return _objectName; } set { _objectName = " -n " + value; } }
        public string VersionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
        public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string OutputOptionTypes { get { return _outputOptionTypes; } set { _outputOptionTypes = " -p " + value; } }
        public string PersistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
        public string PersistentOutputFileName { get { return _persistentOutputFileName; } set { _persistentOutputFileName = " -u " + value; } }
        public string EndOfRecordSeparator { get { return _endOfRecordSeparator; } set { _endOfRecordSeparator = " -r " + value; } }
        public string EndOfListingIndicator { get { return _endOfListingIndicator; } set { _endOfListingIndicator = " -l " + value; } }
    }
}