namespace IPCUtilities.IpcPmrep
{

        public class PmrepPermissions : AbstractRepoObject
        {
            private string _userName;
            private string _groupName;
            private string _securityDomain;
            private string _permission;

            public string userName { get { return _userName; } set { _userName = " -u " + value; } }
            public string groupName { get { return _groupName; } set { _groupName = " -g " + value; } }
            public string securityDomain { get { return _securityDomain; } set { _securityDomain = " -s " + value; } }
            public string permission { get { return _permission; } set { _permission = " -p " + value; } }
        }
}