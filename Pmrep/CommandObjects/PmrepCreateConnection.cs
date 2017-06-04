
namespace IPCUtilities
{
    namespace IpcPmrep
    {
        public class PmrepCreateConnection
        {
            private string _connectionType;
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

            public string connectionType { get { return _connectionType; } set { _connectionType = " -s " + value; } }
            public string connectionName { get { return _connectionName; } set { _connectionName = " -n " + value; } }
            public string userName { get { return _userName; } set { _userName = " -u " + value; } }
            public string password { get { return _password; } set { _password = " -p " + value; } }
            public string connectString { get { return _connectString; } set { _connectString = " -c " + value; } }
            public string codePage { get { return _codePage; } set { _codePage = " -l " + value; } }
            public string rollbackSegment { get { return _rollbackSegment; } set { _rollbackSegment = " -r " + value; } }
            public string conectionEnvironmentSQL { get { return _conectionEnvironmentSQL; } set { _conectionEnvironmentSQL = " -e " + value; } }
            public string transactionEnvironmentSQL { get { return _transactionEnvironmentSQL; } set { _transactionEnvironmentSQL = " -f " + value; } }
            public string packetSize { get { return _packetSize; } set { _packetSize = " -z " + value; } }
            public string databaseName { get { return _databaseName; } set { _databaseName = " -b " + value; } }
            public string serverName { get { return _serverName; } set { _serverName = " -v " + value; } }
            public string domainName { get { return _domainName; } set { _domainName = " -d " + value; } }
            public string dataSourceName { get { return _dataSourceName; } set { _dataSourceName = " -a " + value; } }
            public string connectionAttributes { get { return _connectionAttributes; } set { _connectionAttributes = " -k " + value; } }


        }
    }
}