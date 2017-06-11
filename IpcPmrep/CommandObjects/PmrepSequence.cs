namespace IPCUtilities.IpcPmrep
{
    public class PmrepSequence
    {
        private string _folderName;
        private string _mappingName;
        private string _sequenceGeneratorName;
        private string _startValue;
        private string _endValue;
        private string _incrementBy;
        private string _currentValue;

        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string mappingName { get { return _mappingName; } set { _mappingName = " -m " + value; } }
        public string sequenceGeneratorName { get { return _sequenceGeneratorName; } set { _sequenceGeneratorName = " -t " + value; } }
        public string startValue { get { return _startValue; } set { _startValue = " -s " + value; } }
        public string endValue { get { return _endValue; } set { _endValue = " -e " + value; } }
        public string incrementBy { get { return _incrementBy; } set { _incrementBy = " -i " + value; } }
        public string currentValue { get { return _currentValue; } set { _currentValue = " -c " + value; } }
    }
}