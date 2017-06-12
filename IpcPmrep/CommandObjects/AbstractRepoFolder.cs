namespace IPCUtilities.IpcPmrep
{
    public abstract class AbstractRepoFolder
    {
        private string _folder_name;
        private string _folder_description;
        private string _owner_name;
        private string _owner_security_domain;
        private string _shared_folder;
        private string _permissions;
        private string _folder_status;

        public virtual string FolderName { get { return _folder_name; } set { _folder_name = " -n " + value; } }
        public virtual string FolderDescription { get { return _folder_description; } set { _folder_description = " -d " + value; } }
        public virtual string OwnerName { get { return _owner_name; } set { _owner_name = " -o " + value; } }
        public virtual string OwnerSecurityDomain { get { return _owner_security_domain; } set { _owner_security_domain = " -a " + value; } }
        public virtual string SharedFolder { get { return _shared_folder; } set { _shared_folder = " -s " + value; } }
        public virtual string Permissions { get { return _permissions; } set { _permissions = " -p " + value; } }
        public virtual string FolderStatus { get { return _folder_status; } set { _folder_status = " -f " + value; } }
    }
}
