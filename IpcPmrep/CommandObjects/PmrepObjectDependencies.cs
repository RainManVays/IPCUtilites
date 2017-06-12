
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

            public string VersionNumber { get { return _versionNumber; } set { _versionNumber = " -v " + value; } }
            public string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
            public string PersistentInputFile { get { return _persistentInputFile; } set { _persistentInputFile = " -i " + value; } }
            public string DependencyObjectTypes { get { return _dependencyObjectTypes; } set { _dependencyObjectTypes = " -d " + value; } }
            public string DependencyDirection { get { return _dependencyDirection; } set { _dependencyDirection = " -p " + value; } }
            public string PersistentOutputFileName { get { return _persistentOutputFileName; } set { _persistentOutputFileName = " -u " + value; } }
            public string ColumnSeparator { get { return _columnSeparator; } set { _columnSeparator = " -c " + value; } }
            public string EndOfRecordSeparator { get { return _endOfRecordSeparator; } set { _endOfRecordSeparator = " -r " + value; } }
            public string EndOfListingIndicator { get { return _endOfListingIndicator; } set { _endOfListingIndicator = " -l " + value; } }
            public string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
            public bool Append { get; set; }
            public bool Verbose { get; set; }
            public bool PrintDBtype { get; set; }
            public bool AcrossRepositories { get; set; }
            public bool IncludeFkPkDependency { get; set; }
    }
}