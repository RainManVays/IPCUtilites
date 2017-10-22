namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepConnection
    {
        private string _domain;
        private string _repository;
        private string _hostName;
        private string _port;
        private string _userName;
        private string _password;
        private string _timeout;


        public string Domain
        {
            get { return _domain; }
            set { _domain = " -d " + value; }
        }
        public string Repository { get { return _repository; } set { _repository = " -r " + value; } }
        public string HostName { get { return _hostName; } set { _hostName = " -h " + value; } }
        public string Port { get { return _port; } set { _port = " -o " + value; } }
        public string UserName { get { return _userName; } set { _userName = " -n " + value; } }
        public string Password { get { return _password; } set { _password = " -x " + value; } }
        public string Timeout { get { return _timeout; } set { _timeout = " -t " + value; } }
    }
}
