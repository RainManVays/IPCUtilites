namespace IPCUtilities.IpcPmrep.CommandObjects
{
    public class ConnectedUser
    {
        public string ConnectionID { get; set; }
        public string UserName { get; set; }
        public string Application { get; set; }
        public string Service { get; set; }
        public string HostName { get; set; }
        public string HostAdress { get; set; }
        public string LoginTime { get; set; }
        public string LastActivityTime { get; set; }
        public string ProcessID { get; set; }
        public string Status { get; set; }
    }
}