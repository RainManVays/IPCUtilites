namespace IPCUtilities.IpcPmrep
{
    public class PmrepRepoObject
    {
        private string _localRepoName;
        private string _localRepoUser;
        private string _localRepoUserSecurityDomain;
        private string _localRepoPassword;
        private string _localRepoPasswordEnvVar;
        private string _localRepoDomainName;
        private string _localRepoPortalHostName;
        private string _localRepoPortalPort;

        public string localRepoName { get { return _localRepoName; } set { _localRepoName = " -r " + value; } }
        public string localRepoUser { get { return _localRepoUser; } set { _localRepoUser = " -n " + value; } }
        public string localRepoUserSecurityDomain { get { return _localRepoUserSecurityDomain; } set { _localRepoUserSecurityDomain = " -s " + value; } }
        public string localRepoPassword { get { return _localRepoPassword; } set { _localRepoPassword = " -x " + value; } }
        public string localRepoPasswordEnvVar { get { return _localRepoPasswordEnvVar; } set { _localRepoPasswordEnvVar = " -X " + value; } }
        public string localRepoDomainName { get { return _localRepoDomainName; } set { _localRepoDomainName = " -d " + value; } }
        public string localRepoPortalHostName { get { return _localRepoPortalHostName; } set { _localRepoPortalHostName = " -h " + value; } }
        public string localRepoPortalPort { get { return _localRepoPortalPort; } set { _localRepoPortalPort = " -o " + value; } }
    }
}