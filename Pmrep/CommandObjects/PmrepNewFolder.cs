
namespace IPCUtilities
{
    namespace IpcPmrep
    {
        public class PmrepNewFolder
        {
            private string _folder_name;
            private string _folder_description;
            private string _owner_name;
            private string _owner_security_domain;
            private string _shared_folder;
            private string _permissions;
            private string _folder_status;

            public string folderName { get { return _folder_name; } set { _folder_name = " -n " + value; } }
            public string folderDescription { get { return _folder_description; } set { _folder_description = " -d " + value; } }
            public string ownerName { get { return _owner_name; } set { _owner_name = " -o " + value; } }
            public string ownerSecurityDomain { get { return _owner_security_domain; } set { _owner_security_domain = " -a " + value; } }
            public string sharedFolder { get { return _shared_folder; } set { _shared_folder = " -s" + value; } }
            public string permissions { get { return _permissions; } set { _permissions = " -p " + value; } }
            public string folderStatus { get { return _folder_status; } set { _folder_status = " -f " + value; } }

        }
    }
}