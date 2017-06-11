
namespace IPCUtilities
{
    namespace IpcPmrep
    {
        public class PmrepChangeOwner: AbstractRepoObject
        {

            private string _newOwnerName;
            private string _securityDomain;

            public string newOwnerName { get { return _newOwnerName; } set { _newOwnerName = " -u " + value; } }
            public string securityDomain { get { return _securityDomain; } set { _securityDomain = " -s " + value; } }
        }
    }
}