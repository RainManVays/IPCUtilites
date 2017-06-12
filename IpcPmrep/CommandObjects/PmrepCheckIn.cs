namespace IPCUtilities.IpcPmrep
{
        public class PmrepCheckIn:AbstractRepoObject
        {
        private string _folderName;
        private string _comments;
        private string _dbdSeparator;
        public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string Comments { get { return _comments; } set { _comments = " -c " + value; } }
        public string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }

    }
}