namespace IPCUtilities.IpcPmrep
{
    public class PmrepTargPrefix
    {
        private string _folderName;
        private string _sessionName;
        private string _targetName;
        private string _prefixName;

        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string sessionName { get { return _sessionName; } set { _sessionName = " -s " + value; } }
        public string targetName { get { return _targetName; } set { _targetName = " -t " + value; } }
        public string prefixName { get { return _prefixName; } set { _prefixName = " -p " + value; } }
    }
}