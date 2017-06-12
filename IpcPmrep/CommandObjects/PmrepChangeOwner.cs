
namespace IPCUtilities
{
    namespace IpcPmrep
    {
        public class PmrepChangeOwner: AbstractRepoObject
        {

            private string _newOwnerName;
            private string _securityDomain;

            public string NewOwnerName { get { return _newOwnerName; } set { _newOwnerName = " -u " + value; } }
            public string SecurityDomain { get { return _securityDomain; } set { _securityDomain = " -s " + value; } }
        }
    }
}