
namespace IPCUtilities.IpcPmrep
{
        public class PmrepCheckout
        {
            private string _objectType;
            private string _folderName;
            private string _columnSeparator;
            private string _endOfRecordSeparator;
            private string _endOfListingIndicator;
            private string _dbdSeparator;


            public  string objectType { get { return _objectType; } set { _objectType = " -o " + value; } }
            public  string folderName { get { return _folderName; } set { _folderName = " -f " + value; } }
            public  string columnSeparator { get { return _columnSeparator; } set { _columnSeparator = " -c " + value; } }
            public  string endOfRecordSeparator { get { return _endOfRecordSeparator; } set { _endOfRecordSeparator = " -r " + value; } }
            public  string endOfListingIndicator { get { return _endOfListingIndicator; } set { _endOfListingIndicator = " -l " + value; } }
            public  string dbdSeparator { get { return _dbdSeparator; } set { _dbdSeparator = " -s " + value; } }
        }
}