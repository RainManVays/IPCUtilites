namespace IPCUtilities.IpcPmrep
{
    public abstract class AbstractRepoObject
    {
        private string _objectName;
        private string _objectType;
        private string _objectSubType;
        public virtual string ObjectName { get { return _objectName; } set { _objectName = " -n " + value; } }
        public virtual string ObjectType { get { return _objectType; } set { _objectType = " -o " + value; } }
        public virtual string ObjectSubType { get { return _objectSubType; } set { _objectSubType = " -t " + value; } }
    }
}
