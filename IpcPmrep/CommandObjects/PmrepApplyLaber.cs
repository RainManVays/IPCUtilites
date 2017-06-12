namespace IPCUtilities.IpcPmrep
{
        public class PmrepApplyLabel : AbstractRepoObject
        {
            private string _labelName;
            private string _versionNumber;
            private string _folderName;
            private string _persistentInputFile;
            private string _dependencyObjectTypes;
            private string _dependencyDirection;
            private string _dbdSeparator;
            public string LabelName { get { return _labelName; } set { _labelName = " -a " + value; } }
            public string VersionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
            public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
            public string PersistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
            public string DependencyObjectTypes { get { return _dependencyObjectTypes; } set { _dependencyObjectTypes = " -d " + value; } }
            public string DependencyDirection { get { return _dependencyDirection; } set { _dependencyDirection = " -p " + value; } }
            public string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -e " + value; } }

    }
}