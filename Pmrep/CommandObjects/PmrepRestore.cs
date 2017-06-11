namespace IPCUtilities.IpcPmrep
{
    public class PmrepRestore
    {
        private string _domainUserName;
        private string _domainUserSecurity;
        private string _domainPassword;
        private string _domainPasswordEnviVar;
        private string _inputFileName;

        public string domainUserName { get { return _domainUserName; } set { _domainUserName = " -u " + value; } }
        public string domainUserSecurity { get { return _domainUserSecurity; } set { _domainUserSecurity = " -s " + value; } }
        public string domainPassword { get { return _domainPassword; } set { _domainPassword = " -p " + value; } }
        public string domainPasswordEnviVar { get { return _domainPasswordEnviVar; } set { _domainPasswordEnviVar = " -P " + value; } }
        public string inputFileName { get { return _inputFileName; } set { _inputFileName = " -i " + value; } }
        public bool createGlobalRepository { get; set; }
        public bool enableObjVersioning { get; set; }
        public bool skipLogs { get; set; }
        public bool skipDeployHistory { get; set; }
        public bool skipMxData { get; set; }
        public bool skipTaskStatistic { get; set; }
        public bool asNewRepository { get; set; }
        public bool exitIfDomainDiffCurr { get; set; }

    }
}