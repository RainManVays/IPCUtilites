
namespace IPCUtilities.IpcPmrep
{
   public class DBType
    {
        private DBType(string value) { Value = value; }
        public string Value { get;}
        public static DBType HttpTransformation { get { return new DBType("\"Http Transformation\""); } }
        public static DBType DB2 { get { return new DBType("DB2"); } }
        public static DBType Informix { get { return new DBType("Informix"); } }
        public static DBType JMSConnection { get { return new DBType("\"JMS Connection\""); } }
        public static DBType JNDIConnection { get { return new DBType("\"JNDI Connection\""); } }
        public static DBType LMAPITarget { get { return new DBType("LMAPITarget"); } }
        public static DBType MicrosoftSQLServer { get { return new DBType("\"Microsoft SQL Server\""); } }
        public static DBType ODBC { get { return new DBType("ODBC"); } }
        public static DBType Oracle { get { return new DBType("Oracle"); } }
        public static DBType PeopleSoftDB2 { get { return new DBType("\"PeopleSoft DB2\""); } }
        public static DBType PeopleSoftInformix { get { return new DBType("\"PeopleSoft Informix\""); } }
        public static DBType PeopleSoftMsSqlserver { get { return new DBType("\"PeopleSoft MsSqlserver\""); } }
        public static DBType PeopleSoftOracle { get { return new DBType("\"PeopleSoft Oracle\""); } }
        public static DBType PeopleSoftSybase { get { return new DBType("\"PeopleSoft Sybase\""); } }
        public static DBType PowerChannelforDB2 { get { return new DBType("\"PowerChannel for DB2\""); } }
        public static DBType PowerChannelforMSSQL { get { return new DBType("\"PowerChannel for MS SQL Server\""); } }
        public static DBType PowerChannelforODBC { get { return new DBType("\"PowerChannel for ODBC\""); } }
        public static DBType PowerChannelforOracle { get { return new DBType("\"PowerChannel for Oracle\""); } }
        public static DBType PWXDB2i5OS { get { return new DBType("\"PWX DB2i5OS\""); } }
        public static DBType PWXDB2i5OS_CDC_Change { get { return new DBType("\"PWX DB2i5OS CDC Change\""); } }
        public static DBType PWXDB2i5OS_CDC_RealTime { get { return new DBType("\"PWX DB2i5OS CDC Real Time\""); } }
        public static DBType PWXDB2LUW_CDC_Change { get { return new DBType("\"PWX DB2LUW CDC Change\""); } }
        public static DBType PWXDB2LUW_CDC_RealTime { get { return new DBType("\"PWX DB2LUW CDC Real Time\""); } }
        public static DBType PWXNRDB_Batch { get { return new DBType("\"PWX NRDB Batch\""); } }
        public static DBType PWXNRDB_CDC_Change { get { return new DBType("\"PWX NRDB CDC Change\""); } }
        public static DBType PWXNRDB_CDC_RealTime { get { return new DBType("\"PWX NRDB CDC Real Time\""); } }
        public static DBType PWXNRDB_Lookup { get { return new DBType("\"PWX NRDB Lookup\""); } }
        public static DBType PWXOracle { get { return new DBType("\"PWX Oracle\""); } }
        public static DBType PWXOracle_CDC_Change { get { return new DBType("\"PWX Oracle CDC Change\""); } }
        public static DBType PWXOracle_CDC_RealTime { get { return new DBType("\"PWX Oracle CDC Real Time\""); } }
        public static DBType PWXSybase { get { return new DBType("\"PWX Sybase\""); } }
        public static DBType SalesforceConnection { get { return new DBType("\"Salesforce Connection\""); } }
        public static DBType SAP_BW { get { return new DBType("\"SAP BW\""); } }
        public static DBType SAP_BWOHS_READER { get { return new DBType("SAP_BWOHS_READER"); } }
        public static DBType SAPR3 { get { return new DBType("\"SAP R3\""); } }
        public static DBType SAP_RFC_BAPI_Interface { get { return new DBType("\"SAP RFC/BAPI Interface\""); } }
        public static DBType SAP_ALE_IDoc_Reader { get { return new DBType("SAP_ALE_IDoc_Reader"); } }
        public static DBType SAP_ALE_IDoc_Writer { get { return new DBType("SAP_ALE_IDoc_Writer"); } }
        public static DBType SiebelDB2 { get { return new DBType("\"Siebel DB2\""); } }
        public static DBType SiebelInformix { get { return new DBType("\"Siebel Informix\""); } }
        public static DBType SiebelMsSqlserver { get { return new DBType("\"Siebel MsSqlserver\""); } }
        public static DBType SiebelOracle { get { return new DBType("\"Siebel Oracle\""); } }
        public static DBType SiebelSybase { get { return new DBType("\"Siebel Sybase\""); } }
        public static DBType Sybase { get { return new DBType("Sybase"); } }
        public static DBType Teradata { get { return new DBType("Teradata"); } }
        public static DBType TeradataFastExportConnection { get { return new DBType("\"Teradata FastExport Connection\""); } }
        public static DBType WebServicesConsumer { get { return new DBType("\"Web Services Consumer\""); } }
        public static DBType webMethodsBroker { get { return new DBType("\"webMethods Broker\""); } }

    }
}
