
namespace IPCUtilities
{
    namespace IpcPmrep
    {
        public class PmrepAddDeploymentGroup
        {
            private string _deploymentGroupName;
            private string _objectName;
            private string _objectType;
            private string _objectSubType;
            private string _versionNumber;
            private string _folderName;
            private string _persistentImputFile;
            private string _dependencyTypes;
            private string _dbdSeparator;

            public string deploymentGroupName { get { return _deploymentGroupName; } set { _deploymentGroupName = " -p " + value; } }
            public string objectName { get { return _objectName; } set { _objectName = " -n " + value; } }
            public string objectType { get { return _objectType; } set { _objectType = " -o " + value; } }
            public string objectSubType { get { return _objectSubType; } set { _objectSubType = " -t " + value; } }
            public string versionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
            public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
            public string persistentImputFile { get { return _persistentImputFile; } set { _persistentImputFile = " -i " + value; } }
            public string dependencyTypes { get { return _dependencyTypes; } set { _dependencyTypes = " -d " + value; } }
            public string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
        }
    }
}