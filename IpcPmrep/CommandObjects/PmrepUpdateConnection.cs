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


        public string ConnectionSubtype { get { return _connectionSubtype; } set { _connectionSubtype = " -t " + value; } }
        public string ConnectionName { get { return _connectionName; } set { _connectionName = " -d " + value; } }
        public string NewUserName { get { return _newUserName; } set { _newUserName = " -u " + value; } }
        public string NewPassword { get { return _newPassword; } set { _newPassword = " -p " + value; } }
        public string NewPasswordEnvVariable { get { return _newPasswordEnvVariable; } set { _newPasswordEnvVariable = " -P " + value; } }
        public string NewConnectionString { get { return _newConnectionString; } set { _newConnectionString = " -c " + value; } }
        public string AttributeName { get { return _attributeName; } set { _attributeName = " -a " + value; } }
        public string NewAttributeValue { get { return _newAttributeValue; } set { _newAttributeValue = " -v " + value; } }
        public string ConnectionType { get { return _connectionType; } set { _connectionType = " -s " + value; } }
        public string CodePage { get { return _codePage; } set { _codePage = " -l " + value; } }

    }
}