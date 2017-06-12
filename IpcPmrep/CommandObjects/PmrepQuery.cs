
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

            public virtual string QueryName { get { return _queryName; } set { _queryName = " -q " + value; } }
            public virtual string QueryType { get { return _queryType; } set { _queryType = " -t " + value; } }
            public virtual string OutputPersistentFileName { get { return _outputPersistentFileName; } set { _outputPersistentFileName = " -u " + value; } }
            public virtual string ColumnSeparator { get { return _columnSeparator; } set { _columnSeparator = " -c " + value; } }
            public virtual string EndOfRecordSeparator { get { return _endOfRecordSeparator; } set { _endOfRecordSeparator = " -r " + value; } }
            public virtual string EndOfListingIndicator { get { return _endOfListingIndicator; } set { _endOfListingIndicator = " -l " + value; } }
            public virtual string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
        }
}