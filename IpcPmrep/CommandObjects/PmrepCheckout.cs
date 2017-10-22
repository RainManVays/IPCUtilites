
namespace IPCUtilities.IpcPmrep.CommandObjects
{
        public class PmrepCheckout
        {
            private string _objectType;
            private string _folderName;
            private string _columnSeparator;
            private string _endOfRecordSeparator;
            private string _endOfListingIndicator;
            private string _dbdSeparator;


            public  string ObjectType { get { return _objectType; } set { _objectType = " -o " + value; } }
            public  string FolderName { get { return _folderName; } set { _folderName = " -f " + value; } }
            public  string ColumnSeparator { get { return _columnSeparator; } set { _columnSeparator = " -c " + value; } }
            public  string EndOfRecordSeparator { get { return _endOfRecordSeparator; } set { _endOfRecordSeparator = " -r " + value; } }
            public  string EndOfListingIndicator { get { return _endOfListingIndicator; } set { _endOfListingIndicator = " -l " + value; } }
            public  string DbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
        }
}