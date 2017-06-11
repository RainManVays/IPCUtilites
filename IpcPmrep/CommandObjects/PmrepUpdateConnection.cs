namespace IPCUtilities.IpcPmrep
{
    public class PmrepUpdateConnection
    {
        private string _connectionSubtype;
        private string _connectionName;
        private string _newUserName;
        private string _newPassword;
        private string _newPasswordEnvVariable;
        private string _newConnectionString;
        private string _attributeName;
        private string _newAttributeValue;
        private string _connectionType;
        private string _codePage;


        public string connectionSubtype { get { return _connectionSubtype; } set { _connectionSubtype = " -t " + value; } }
        public string connectionName { get { return _connectionName; } set { _connectionName = " -d " + value; } }
        public string newUserName { get { return _newUserName; } set { _newUserName = " -u " + value; } }
        public string newPassword { get { return _newPassword; } set { _newPassword = " -p " + value; } }
        public string newPasswordEnvVariable { get { return _newPasswordEnvVariable; } set { _newPasswordEnvVariable = " -P " + value; } }
        public string newConnectionString { get { return _newConnectionString; } set { _newConnectionString = " -c " + value; } }
        public string attributeName { get { return _attributeName; } set { _attributeName = " -a " + value; } }
        public string newAttributeValue { get { return _newAttributeValue; } set { _newAttributeValue = " -v " + value; } }
        public string connectionType { get { return _connectionType; } set { _connectionType = " -s " + value; } }
        public string codePage { get { return _codePage; } set { _codePage = " -l " + value; } }

    }
}