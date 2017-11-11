namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public abstract class AbstractRepoObject
    {
        private string _objectName;
        public RepoObject ObjectType;
        public RepoObject ObjectSubtype;
        public virtual string ObjectName { get { return _objectName; } set { _objectName = " -n " + value; } }
    }
}
