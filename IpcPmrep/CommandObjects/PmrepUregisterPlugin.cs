namespace IPCUtilities.IpcPmrep
{
    public class PmrepUregisterPlugin
    {
        private string _vendorId;
        private string _pluginId;
        private string _newPassword;
        private string _newPasswordEnvVariable;

        public string vendorId { get { return _vendorId; } set { _vendorId = " -v " + value; } }
        public string pluginId { get { return _pluginId; } set { _pluginId = " -l " + value; } }
        public string newPassword { get { return _newPassword; } set { _newPassword = " -w " + value; } }
        public string newPasswordEnvVariable { get { return _newPasswordEnvVariable; } set { _newPasswordEnvVariable = " -W " + value; } }
    }
}