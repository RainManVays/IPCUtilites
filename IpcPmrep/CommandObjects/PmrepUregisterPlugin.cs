namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class PmrepUregisterPlugin
    {
        private string _vendorId;
        private string _pluginId;
        private string _newPassword;
        private string _newPasswordEnvVariable;

        public string VendorId { get { return _vendorId; } set { _vendorId = " -v " + value; } }
        public string PluginId { get { return _pluginId; } set { _pluginId = " -l " + value; } }
        public string NewPassword { get { return _newPassword; } set { _newPassword = " -w " + value; } }
        public string NewPasswordEnvVariable { get { return _newPasswordEnvVariable; } set { _newPasswordEnvVariable = " -W " + value; } }
    }
}