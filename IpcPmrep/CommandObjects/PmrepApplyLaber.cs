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
            public string labelName { get { return _labelName; } set { _labelName = " -a " + value; } }
            public string versionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
            public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
            public string persistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
            public string dependencyObjectTypes { get { return _dependencyObjectTypes; } set { _dependencyObjectTypes = " -d " + value; } }
            public string dependencyDirection { get { return _dependencyDirection; } set { _dependencyDirection = " -p " + value; } }
            public string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -e " + value; } }

    }
}