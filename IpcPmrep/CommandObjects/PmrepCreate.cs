
namespace IPCUtilities.IpcPmrep
{
        public class PmrepCreate
        {
            private string _domainUserName;
            private string _domainPassword;
            private string _domainUserSecurity;

            public string domainUserName { get { return _domainUserName; } set { _domainUserName = " -u " + value; } }
            public string domainPassword { get { return _domainPassword; } set { _domainPassword = " -p " + value; } }
            public string domainUserSecurity { get { return _domainUserSecurity; } set { _domainUserSecurity = " -s " + value; } }
        }
}