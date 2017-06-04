# IPCUtilites
Framework for Informatica IPC utilites


            Environment.SetEnvironmentVariable("INFA_DOMAINS_FILE", @"C:\Informatica\9.6.1\clients\PowerCenterClient\domain.infa");
            PmrepConnection repo = new PmrepConnection
            {
                domain = "Domain_melchior",
                repository = "infa_rep_melchior",
                userName = "Administrator",
                password = "k2kb2bd"
            };
            Pmrep pmrep = new Pmrep("C:\\Informatica\\9.6.1\\clients\\PowerCenterClient\\client\\bin\\pmrep.exe", repo);

            pmrep.CreateFolder("TEST");
            var result=pmrep.ListObjects(new PmrepObject { objectType = "folder" });
