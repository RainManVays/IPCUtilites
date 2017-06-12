namespace IPCUtilities.IpcPmrep
{
    public class PmrepRegisterPlugin
    {
        private string _inputRegistrationFile;
        private string _NISLogin;
        private string _NISPassword;
        private string _NISPasswordEnviVar;

        public string InputRegistrationFile { get { return _inputRegistrationFile; } set { _inputRegistrationFile = " -i " + value; } }
        public string NISLogin { get { return _NISLogin; } set { _NISLogin = " -l " + value; } }
        public string NISPassword { get { return _NISPassword; } set { _NISPassword = " -w " + value; } }
        public string NISPasswordEnviVar { get { return _NISPasswordEnviVar; } set { _NISPasswordEnviVar = " -W " + value; } }
    }
}