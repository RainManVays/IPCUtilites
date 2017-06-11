namespace IPCUtilities.IpcPmrep
{
   public class ConnectionType
    {
        private ConnectionType(string value) { Value = value; }
        public string Value { get; set; }
        public static ConnectionType Relational { get { return new ConnectionType("Relational"); } }
        public static ConnectionType FTP { get { return new ConnectionType("FTP"); } }
        public static ConnectionType Application { get { return new ConnectionType("Application"); } }
        public static ConnectionType Loader { get { return new ConnectionType("Loader"); } }
        public static ConnectionType Queue { get { return new ConnectionType("Queue"); } }
    }
}
