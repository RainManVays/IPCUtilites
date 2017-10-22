namespace IPCUtilities.IpcPmrep.CommandObjects
{
        public class PmrepAddDeploymentGroup: AbstractRepoObject
        {
            private string _deploymentGroupName;
            private string _versionNumber;
            private string _folderName;
            private string _persistentImputFile;
            private string _dependencyTypes;
            private string _dbdSeparator;

            public string DeploymentGroupName { get { return _deploymentGroupName; } set { _deploymentGroupName = " -p " + value; } }
            public string VersionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
            public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
            public string PersistentImputFile { get { return _persistentImputFile; } set { _persistentImputFile = " -i " + value; } }
            public string DependencyTypes { get { return _dependencyTypes; } set { _dependencyTypes = " -d " + value; } }
            public string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
        }
}