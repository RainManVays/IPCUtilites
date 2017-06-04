
namespace IPCUtilities
{
    namespace IpcPmrep
    {
        public class PmrepNewDeploymentGroup
        {
            private string _deploymentGroupName;
            private string _deploymentGroupType;
            private string _queryName;
            private string _queryType;
            private string _comments;

            public string deploymentGroupName { get { return _deploymentGroupName; } set { _deploymentGroupName = " -p " + value; } }
            public string deploymentGroupType { get { return _deploymentGroupType; } set { _deploymentGroupType = " -t " + value; } }
            public string queryName { get { return _queryName; } set { _queryName = " -q " + value; } }
            public string queryType { get { return _queryType; } set { _queryType = " -u " + value; } }
            public string comments { get { return _comments; } set { _comments = " -c " + value; } }
        }
    }
}