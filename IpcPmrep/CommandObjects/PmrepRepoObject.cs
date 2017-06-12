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

        public string LocalRepoName { get { return _localRepoName; } set { _localRepoName = " -r " + value; } }
        public string LocalRepoUser { get { return _localRepoUser; } set { _localRepoUser = " -n " + value; } }
        public string LocalRepoUserSecurityDomain { get { return _localRepoUserSecurityDomain; } set { _localRepoUserSecurityDomain = " -s " + value; } }
        public string LocalRepoPassword { get { return _localRepoPassword; } set { _localRepoPassword = " -x " + value; } }
        public string LocalRepoPasswordEnvVar { get { return _localRepoPasswordEnvVar; } set { _localRepoPasswordEnvVar = " -X " + value; } }
        public string LocalRepoDomainName { get { return _localRepoDomainName; } set { _localRepoDomainName = " -d " + value; } }
        public string LocalRepoPortalHostName { get { return _localRepoPortalHostName; } set { _localRepoPortalHostName = " -h " + value; } }
        public string LocalRepoPortalPort { get { return _localRepoPortalPort; } set { _localRepoPortalPort = " -o " + value; } }
    }
}