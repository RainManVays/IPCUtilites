
namespace IPCUtilities.IpcPmrep
{

        public class PmrepRunDeploymentGroup
        {
            private string _deploymentGroupName;
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

            public virtual string DeploymentGroupName { get { return _deploymentGroupName; } set { _deploymentGroupName = " -p " + value; } }
            public virtual string ControlFileName { get { return _controlFileName; } set { _controlFileName = " -c " + value; } }
            public virtual string TargetRepositoryName { get { return _targetRepositoryName; } set { _targetRepositoryName = " -r " + value; } }
            public virtual string TargetRepositoryUser { get { return _targetRepositoryUser; } set { _targetRepositoryUser = " -n " + value; } }
            public virtual string TargetRepositoryUserSecurityDomain { get { return _targetRepositoryUserSecurityDomain; } set { _targetRepositoryUserSecurityDomain = " -s " + value; } }
            public virtual string TargetRepositoryPassword { get { return _targetRepositoryPassword; } set { _targetRepositoryPassword = " -x " + value; } }
            public virtual string TargetRepositoryPasswordEnvVar { get { return _targetRepositoryPasswordEnvVar; } set { _targetRepositoryPasswordEnvVar = " -X " + value; } }
            public virtual string TargetDomainName { get { return _targetDomainName; } set { _targetDomainName = " -d " + value; } }
            public virtual string TargetPortalHostName { get { return _targetPortalHostName; } set { _targetPortalHostName = " -h " + value; } }
            public virtual string TargetPortalPortNumber { get { return _targetPortalPortNumber; } set { _targetPortalPortNumber = " -o " + value; } }
            public virtual string LogFileName { get { return _logFileName; } set { _logFileName = " -l " + value; } }
    }
}