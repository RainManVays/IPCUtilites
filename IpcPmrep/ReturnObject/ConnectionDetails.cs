namespace IPCUtilities.IpcPmrep
{
    public class ConnectionDetails
    {
        public ConnectionType connectType { get; set; }
        public string Name { get; set; }
        public string DbType { get; set; }
        public string UserName { get; set; }
        public string ConnectString { get; set; }
        public string CodePage { get; set; }
        public string RollbackSegment { get; set; }
        public string ConnectEnvSql { get; set; }
        public string TransactionEnvSql { get; set; }
        public int EnableParralelMode { get; set; }
        public int RetryPeriod { get; set; }

    }
}
