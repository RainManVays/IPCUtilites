namespace IPCUtilities.IpcPmrep
{
    public class PmrepUndoCheckout: AbstractRepoObject
    {

        private string _folderName;
        private string _dbdSeparator;

        public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }

    }
}