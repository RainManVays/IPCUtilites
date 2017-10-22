namespace IPCUtilities.IpcPmrep.CommandObjects
{
        public class PmrepCreate
        {
            private string _domainUserName;
            private string _domainPassword;
            private string _domainUserSecurity;

            public string DomainUserName { get { return _domainUserName; } set { _domainUserName = " -u " + value; } }
            public string DomainPassword { get { return _domainPassword; } set { _domainPassword = " -p " + value; } }
            public string DomainUserSecurity { get { return _domainUserSecurity; } set { _domainUserSecurity = " -s " + value; } }
        }
}