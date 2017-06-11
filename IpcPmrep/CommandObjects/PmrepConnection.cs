
namespace IPCUtilities
{
    namespace IpcPmrep
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


            public string domain
            {
                get { return _domain; }
                set { _domain = " -d " + value; }
            }
            public string repository { get { return _repository; } set { _repository = " -r " + value; } }
            public string hostName { get { return _hostName; } set { _hostName = " -h " + value; } }
            public string port { get { return _port; } set { _port = " -o " + value; } }
            public string userName { get { return _userName; } set { _userName = " -n " + value; } }
            public string password { get { return _password; } set { _password = " -x " + value; } }
            public string timeout { get { return _timeout; } set { _timeout = " -t " + value; } }
        }
    }
}