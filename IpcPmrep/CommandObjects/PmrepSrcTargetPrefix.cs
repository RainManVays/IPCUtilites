namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepSrcTargetPrefix
    {
        private string _folderName;
        private string _sessionName;
        private string _srcTrgName;
        private string _prefixName;

        public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string SessionName { get { return _sessionName; } set { _sessionName = " -s " + value; } }
        public string SrcTrgName { get { return _srcTrgName; } set { _srcTrgName = " -t " + value; } }
        public string PrefixName { get { return _prefixName; } set { _prefixName = " -p " + value; } }
    }
}