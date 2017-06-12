namespace IPCUtilities.IpcPmrep
{
    public class PmrepRestore
    {
        private string _domainUserName;
        private string _domainUserSecurity;
        private string _domainPassword;
        private string _domainPasswordEnviVar;
        private string _inputFileName;

        public string DomainUserName { get { return _domainUserName; } set { _domainUserName = " -u " + value; } }
        public string DomainUserSecurity { get { return _domainUserSecurity; } set { _domainUserSecurity = " -s " + value; } }
        public string DomainPassword { get { return _domainPassword; } set { _domainPassword = " -p " + value; } }
        public string DomainPasswordEnviVar { get { return _domainPasswordEnviVar; } set { _domainPasswordEnviVar = " -P " + value; } }
        public string InputFileName { get { return _inputFileName; } set { _inputFileName = " -i " + value; } }
        public bool CreateGlobalRepository { get; set; }
        public bool EnableObjVersioning { get; set; }
        public bool SkipLogs { get; set; }
        public bool SkipDeployHistory { get; set; }
        public bool SkipMxData { get; set; }
        public bool SkipTaskStatistic { get; set; }
        public bool AsNewRepository { get; set; }
        public bool ExitIfDomainDiffCurr { get; set; }

    }
}