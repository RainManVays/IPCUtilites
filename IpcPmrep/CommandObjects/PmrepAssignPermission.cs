﻿namespace IPCUtilities.IpcPmrep.CommandObjects
{

        public class PmrepPermissions : AbstractRepoObject
        {
            private string _userName;
            private string _groupName;
            private string _securityDomain;
            private string _permission;

            public string UserName { get { return _userName; } set { _userName = " -u " + value; } }
            public string GroupName { get { return _groupName; } set { _groupName = " -g " + value; } }
            public string SecurityDomain { get { return _securityDomain; } set { _securityDomain = " -s " + value; } }
            public string Permission { get { return _permission; } set { _permission = " -p " + value; } }
        }
}