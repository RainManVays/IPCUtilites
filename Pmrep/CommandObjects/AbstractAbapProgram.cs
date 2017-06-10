namespace IPCUtilities.IpcPmrep
{
    public class AbstractAbapProgram
    {
        private string _folderName;
        private string _mappingName;
        private string _versionNumber;
        private string _logFilename;
        private string _userName;
        private string _password;
        private string _connectString;
        private string _client;
        private string _language;
        private string _programMode;
        private string _overrideName;
        private string _developmentClassName;

        public virtual string folderName { get { return _folderName; } set { _folderName = " -s " + value; } }
        public virtual string mappingName { get { return _mappingName; } set { _mappingName = " -m " + value; } }
        public virtual string versionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
        public virtual string logFilename { get { return _logFilename; } set { _logFilename = " -l " + value; } }
        public virtual string userName { get { return _userName; } set { _userName = " -u " + value; } }
        public virtual string password { get { return _password; } set { _password = " -x " + value; } }
        public virtual string connectString { get { return _connectString; } set { _connectString = " -c " + value; } }
        public virtual string client { get { return _client; } set { _client = " -t " + value; } }
        public virtual string language { get { return _language; } set { _language = " -y " + value; } }
        public virtual string programMode { get { return _programMode; } set { _programMode = " -p " + value; } }
        public virtual string overrideName { get { return _overrideName; } set { _overrideName = " -o " + value; } }
    }
}
