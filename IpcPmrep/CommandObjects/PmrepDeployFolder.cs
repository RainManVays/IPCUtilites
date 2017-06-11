
namespace IPCUtilities.IpcPmrep
{
 
   public class PmrepDeployFolder
    {
        private string _folderName;
        private string _controlFileName;
        private string _targetRepositoryName;
        private string _targetRepositoryUser;
        private string _targetRepositoryUserSecurityDomain;
        private string _targetRepositoryPassword;
        private string _targetRepositoryPasswordEnvVar;
        private string _targetDomainName;
        private string _targetPortalHostName;
        private string _targetPortalPortNumber;
        private string _logFileName;

        public  string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
        public  string controlFileName { get { return _controlFileName; } set { _controlFileName = " -c " + value; } }
        public  string targetRepositoryName { get { return _targetRepositoryName; } set { _targetRepositoryName = " -r " + value; } }
        public  string targetRepositoryUser { get { return _targetRepositoryUser; } set { _targetRepositoryUser = " -n " + value; } }
        public  string targetRepositoryUserSecurityDomain { get { return _targetRepositoryUserSecurityDomain; } set { _targetRepositoryUserSecurityDomain = " -s " + value; } }
        public  string targetRepositoryPassword { get { return _targetRepositoryPassword; } set { _targetRepositoryPassword = " -x " + value; } }
        public  string targetRepositoryPasswordEnvVar { get { return _targetRepositoryPasswordEnvVar; } set { _targetRepositoryPasswordEnvVar = " -X " + value; } }
        public  string targetDomainName { get { return _targetDomainName; } set { _targetDomainName = " -d " + value; } }
        public  string targetPortalHostName { get { return _targetPortalHostName; } set { _targetPortalHostName = " -h " + value; } }
        public  string targetPortalPortNumber { get { return _targetPortalPortNumber; } set { _targetPortalPortNumber = " -o " + value; } }
        public  string logFileName { get { return _logFileName; } set { _logFileName = " -l " + value; } }
    }
}