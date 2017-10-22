namespace IPCUtilities.IpcPmcmd.CommandObjects
{
    public class PmcmdConnection
    {
        private string _domain;
        private string _service;
        private string _userName;
        private string _password;
        private string _timeout;


        public string Domain { get { return _domain; } set { _domain = " -domain " + value; }}
        public string Service { get { return _service; } set { _service = " -service " + value; } }
        public string UserName { get { return _userName; } set { _userName = " -user " + value; } }
        public string Password { get { return _password; } set { _password = " -password " + value; } }
        public string Timeout { get { return _timeout; } set { _timeout = " -t " + value; } }
    }
}
