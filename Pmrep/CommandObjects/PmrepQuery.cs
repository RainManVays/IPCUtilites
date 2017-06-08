
namespace IPCUtilities.IpcPmrep
{
        public class PmrepQuery
        {
            private string _queryName;
            private string _queryType;
            private string _outputPersistentFileName;
            private string _columnSeparator;
            private string _endOfRecordSeparator;
            private string _endOfListingIndicator;
            private string _dbdSeparator;

            public virtual string queryName { get { return _queryName; } set { _queryName = " -q " + value; } }
            public virtual string queryType { get { return _queryType; } set { _queryType = " -t " + value; } }
            public virtual string outputPersistentFileName { get { return _outputPersistentFileName; } set { _outputPersistentFileName = " -u " + value; } }
            public virtual string columnSeparator { get { return _columnSeparator; } set { _columnSeparator = " -c " + value; } }
            public virtual string endOfRecordSeparator { get { return _endOfRecordSeparator; } set { _endOfRecordSeparator = " -r " + value; } }
            public virtual string endOfListingIndicator { get { return _endOfListingIndicator; } set { _endOfListingIndicator = " -l " + value; } }
            public virtual string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
        }
}