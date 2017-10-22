namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepCreateConnection
    {
        private string _connectionName;
        private string _userName;
        private string _password;
        private string _connectString;
        private string _codePage;
        private string _rollbackSegment;
        private string _conectionEnvironmentSQL;
        private string _transactionEnvironmentSQL;
        private string _packetSize;
        private string _databaseName;
        private string _serverName;
        private string _domainName;
        private string _dataSourceName;
        private string _connectionAttributes;

        public DBType ConnectionType { get; set; }
        public string ConnectionName { get { return _connectionName; } set { _connectionName = " -n " + value; } }
        public string UserName { get { return _userName; } set { _userName = " -u " + value; } }
        public string Password { get { return _password; } set { _password = " -p " + value; } }
        public string ConnectString { get { return _connectString; } set { _connectString = " -c " + value; } }
        public string CodePage { get { return _codePage; } set { _codePage = " -l " + value; } }
        public string RollbackSegment { get { return _rollbackSegment; } set { _rollbackSegment = " -r " + value; } }
        public string ConectionEnvironmentSQL { get { return _conectionEnvironmentSQL; } set { _conectionEnvironmentSQL = " -e " + value; } }
        public string TransactionEnvironmentSQL { get { return _transactionEnvironmentSQL; } set { _transactionEnvironmentSQL = " -f " + value; } }
        public string PacketSize { get { return _packetSize; } set { _packetSize = " -z " + value; } }
        public string DatabaseName { get { return _databaseName; } set { _databaseName = " -b " + value; } }
        public string ServerName { get { return _serverName; } set { _serverName = " -v " + value; } }
        public string DomainName { get { return _domainName; } set { _domainName = " -d " + value; } }
        public string DataSourceName { get { return _dataSourceName; } set { _dataSourceName = " -a " + value; } }
        public string ConnectionAttributes { get { return _connectionAttributes; } set { _connectionAttributes = " -k " + value; } }


    }
}