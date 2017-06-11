namespace IPCUtilities.IpcPmrep
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
        private string _updateSessionInstanceFlag;
        private string _testMode;
        private string _outputLogFileName;

        public string sessionPropertyType { get { return _sessionPropertyType; } set { _sessionPropertyType = " -t " + value; } }
        public string sessionPropertyName { get { return _sessionPropertyName; } set { _sessionPropertyName = " -n " + value; } }
        public string sessionPropertyValue { get { return _sessionPropertyValue; } set { _sessionPropertyValue = " -v " + value; } }
        public string transformationType { get { return _transformationType; } set { _transformationType = " -w " + value; } }
        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string persistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
        public string conditionOperator { get { return _conditionOperator; } set { _conditionOperator = " -o " + value; } }
        public string conditionValue { get { return _conditionValue; } set { _conditionValue = " -l " + value; } }
        public string updateSessionInstanceFlag { get { return _updateSessionInstanceFlag; } set { _updateSessionInstanceFlag = " -g " + value; } }
        public string testMode { get { return _testMode; } set { _testMode = " -m " + value; } }
        public string outputLogFileName { get { return _outputLogFileName; } set { _outputLogFileName = " -u " + value; } }
    }
}