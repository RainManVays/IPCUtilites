namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepModifyFolder:AbstractRepoFolder
    {
        private string _newFolderName;
        private string _osProfile;

        public string NewFolderName { get { return _newFolderName; } set { _newFolderName = " -r " + value; } }
        public string OsProfile { get { return _osProfile; } set { _osProfile = " -u " + value; } }
    }
}