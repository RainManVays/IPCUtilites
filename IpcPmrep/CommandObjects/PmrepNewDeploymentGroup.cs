namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepNewDeploymentGroup
    {
        private string _deploymentGroupName;
        private string _deploymentGroupType;
        private string _queryName;
        private string _queryType;
        private string _comments;

        public string DeploymentGroupName { get { return _deploymentGroupName; } set { _deploymentGroupName = " -p " + value; } }
        public string DeploymentGroupType { get { return _deploymentGroupType; } set { _deploymentGroupType = " -t " + value; } }
        public string QueryName { get { return _queryName; } set { _queryName = " -q " + value; } }
        public string QueryType { get { return _queryType; } set { _queryType = " -u " + value; } }
        public string Comments { get { return _comments; } set { _comments = " -c " + value; } }
    }
}