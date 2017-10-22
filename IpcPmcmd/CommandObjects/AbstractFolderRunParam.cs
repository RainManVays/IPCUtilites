namespace IPCUtilities.IpcPmcmd.CommandObjects
{
    public class AbstractFolderRunParam
    {
        private string _folder;
        private string _runinsName;
        public virtual string Folder { get { return _folder; } set { _folder = " -folder " + value; } }
        public virtual string RunInsName { get { return _runinsName; } set { _runinsName = " -runinsname " + value; } }
    }
}
