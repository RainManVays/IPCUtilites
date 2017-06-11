namespace IPCUtilities.IpcPmrep
{
        public class PmrepCheckIn:AbstractRepoObject
        {
        private string _folderName;
        private string _comments;
        private string _dbdSeparator;
        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string comments { get { return _comments; } set { _comments = " -c " + value; } }
        public string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }

    }
}