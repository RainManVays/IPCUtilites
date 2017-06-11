namespace IPCUtilities.IpcPmrep
{
    public class PmrepValidate
    {
        private string _objectName;
        private string _objectType;
        private string _versionNumber;
        private string _folderName;
        private string _persistentInputFile;
        private string _outputOptionTypes;
        private string _persistentOutputFileName;
        private string _endOfRecordSeparator;
        private string _endOfListingIndicator;

        public string objectName { get { return _objectName; } set { _objectName = " -n " + value; } }
        public string objectType { get { return _objectType; } set { _objectType = " -o " + value; } }
        public string versionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string outputOptionTypes { get { return _outputOptionTypes; } set { _outputOptionTypes = " -p " + value; } }
        public string persistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
        public string persistentOutputFileName { get { return _persistentOutputFileName; } set { _persistentOutputFileName = " -u " + value; } }
        public string endOfRecordSeparator { get { return _endOfRecordSeparator; } set { _endOfRecordSeparator = " -r " + value; } }
        public string endOfListingIndicator { get { return _endOfListingIndicator; } set { _endOfListingIndicator = " -l " + value; } }
    }
}