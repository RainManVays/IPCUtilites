namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public abstract class AbstractAbapProgram
    {
        private string _folderName;
        private string _mappingName;
        private string _versionNumber;
        private string _logFileName;
        private string _userName;
        private string _password;
        private string _connectString;
        private string _client;
        private string _language;
        private string _programMode;
        private string _overrideName;

        public virtual string FolderName { get { return _folderName; } set { _folderName = " -s " + value; } }
        public virtual string MappingName { get { return _mappingName; } set { _mappingName = " -m " + value; } }
        public virtual string VersionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
        public virtual string LogFileName { get { return _logFileName; } set { _logFileName = " -l " + value; } }
        public virtual string UserName { get { return _userName; } set { _userName = " -u " + value; } }
        public virtual string Password { get { return _password; } set { _password = " -x " + value; } }
        public virtual string ConnectString { get { return _connectString; } set { _connectString = " -c " + value; } }
        public virtual string Client { get { return _client; } set { _client = " -t " + value; } }
        public virtual string Language { get { return _language; } set { _language = " -y " + value; } }
        public virtual string ProgramMode { get { return _programMode; } set { _programMode = " -p " + value; } }
        public virtual string OverrideName { get { return _overrideName; } set { _overrideName = " -o " + value; } }
    }
}
