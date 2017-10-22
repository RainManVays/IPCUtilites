namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepUnistallAbapProgram
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

        public string FolderName { get { return _folderName; } set { _folderName = " -s " + value; } }
        public string MappingName { get { return _mappingName; } set { _mappingName = " -m " + value; } }
        public string VersionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
        public string LogFilename { get { return _logFilename; } set { _logFilename = " -l " + value; } }
        public string UserName { get { return _userName; } set { _userName = " -u " + value; } }
        public string Password { get { return _password; } set { _password = " -x " + value; } }
        public string ConnectString { get { return _connectString; } set { _connectString = " -c " + value; } }
        public string Client { get { return _client; } set { _client = " -t " + value; } }
        public string Language { get { return _language; } set { _language = " -y " + value; } }
        public string ProgramMode { get { return _programMode; } set { _programMode = " -p " + value; } }
    }
}