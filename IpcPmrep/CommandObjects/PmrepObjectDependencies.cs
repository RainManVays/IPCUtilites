
namespace IPCUtilities.IpcPmrep
{
        public class PmrepObjectDependencies:AbstractRepoObject
        {
            private string _versionNumber;
            private string _folderName;
            private string _persistentInputFile;
            private string _dependencyObjectTypes;
            private string _dependencyDirection;
            private string _persistentOutputFileName;
            private string _columnSeparator;
            private string _endOfRecordSeparator;
            private string _endOfListingIndicator;
            private string _dbdSeparator;

            public string versionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
            public string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
            public string persistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
            public string dependencyObjectTypes { get { return _dependencyObjectTypes; } set { _dependencyObjectTypes = " -d " + value; } }
            public string dependencyDirection { get { return _dependencyDirection; } set { _dependencyDirection = " -p " + value; } }
            public string persistentOutputFileName { get { return _persistentOutputFileName; } set { _persistentOutputFileName = " -u " + value; } }
            public string columnSeparator { get { return _columnSeparator; } set { _columnSeparator = " -c " + value; } }
            public string endOfRecordSeparator { get { return _endOfRecordSeparator; } set { _endOfRecordSeparator = " -r " + value; } }
            public string endOfListingIndicator { get { return _endOfListingIndicator; } set { _endOfListingIndicator = " -l " + value; } }
            public string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
            public bool append { get; set; }
            public bool verbose { get; set; }
            public bool printDBtype { get; set; }
            public bool acrossRepositories { get; set; }
            public bool includeFkPkDependency { get; set; }
    }
}