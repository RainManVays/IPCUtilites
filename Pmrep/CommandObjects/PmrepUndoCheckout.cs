namespace IPCUtilities.IpcPmrep
{
    public class PmrepUndoCheckout: AbstractRepoObject
    {

        private string _folderName;
        private string _dbdSeparator;

        public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }

    }
}