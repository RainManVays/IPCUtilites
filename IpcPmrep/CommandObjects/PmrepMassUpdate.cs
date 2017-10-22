namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepMassUpdate
    {
        private string _sessionPropertyType;
        private string _sessionPropertyName;
        private string _sessionPropertyValue;
        private string _transformationType;
        private string _persistentInputFile;
        private string _folderName;
        private string _conditionOperator;
        private string _conditionValue;
        private string _updateSessionInstance;
        private string _testMode;
        private string _outputLogFileName;

        public string SessionPropertyType { get { return _sessionPropertyType; } set { _sessionPropertyType = " -t " + value; } }
        public string SessionPropertyName { get { return _sessionPropertyName; } set { _sessionPropertyName = " -n " + value; } }
        public string SessionPropertyValue { get { return _sessionPropertyValue; } set { _sessionPropertyValue = " -v " + value; } }
        public string TransformationType { get { return _transformationType; } set { _transformationType = " -w " + value; } }
        public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string PersistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
        public string ConditionOperator { get { return _conditionOperator; } set { _conditionOperator = " -o " + value; } }
        public string ConditionValue { get { return _conditionValue; } set { _conditionValue = " -l " + value; } }
        public string UpdateSessionInstance { get { return _updateSessionInstance; } set { _updateSessionInstance = " -g " + value; } }
        public string TestMode { get { return _testMode; } set { _testMode = " -m " + value; } }
        public string OutputLogFileName { get { return _outputLogFileName; } set { _outputLogFileName = " -u " + value; } }
    }
}