namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepObject
    {
        private string _object_type;
        private string _object_subtype;
        private string _folder_name;
        private string _column_separator;
        private string _end_of_record_indicator;
        private string _end_of_listing_indicator;
        private string _verbose;
        private string _print_database_type;
        private string _dbd_separator;

        public string ObjectType { get { return _object_type; } set { _object_type = " -o " + value; } }
        public string ObjectSubtype { get { return _object_subtype; } set { _object_subtype = " -t " + value; } }
        public string FolderName { get { return _folder_name; } set { _folder_name = " -f " + value; } }
        public string ColumnSeparator { get { return _column_separator; } set { _column_separator = " -c " + value; } }
        public string EndOfRecordIndicator { get { return _end_of_record_indicator; } set { _end_of_record_indicator = " -r " + value; } }
        public string EndOfListingIndicator { get { return _end_of_listing_indicator; } set { _end_of_listing_indicator = " -l " + value; } }
        public string Verbose { get { return _verbose; } set { _verbose = " -b " + value; } }
        public string PrintDatabaseType { get { return _print_database_type; } set { _print_database_type = " -y " + value; } }
        public string DbdSeparator { get { return _dbd_separator; } set { _dbd_separator = " -s " + value; } }

    }
}