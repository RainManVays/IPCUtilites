namespace IPCUtilities.IpcPmrep
{
    public class PmrepSrcPrefix
    {
        private string _folderName;
        private string _sessionName;
        private string _sourceName;
        private string _prefixName;

        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string sessionName { get { return _sessionName; } set { _sessionName = " -s " + value; } }
        public string sourceName { get { return _sourceName; } set { _sourceName = " -t " + value; } }
        public string prefixName { get { return _prefixName; } set { _prefixName = " -p " + value; } }
    }
}