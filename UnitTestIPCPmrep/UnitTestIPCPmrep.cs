using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IPCUtilities.IpcPmrep;
using System.Collections.Generic;
using System.IO;

namespace UnitTestIPCPmrep
{
    [TestClass]
    public class UnitTestIPCPmrep
    {
        //[TestProperty]
            //Environment.SetEnvironmentVariable("INFA_DOMAINS_FILE", @"C:\Informatica\9.6.1\clients\PowerCenterClient\domain.infa");
            PmrepConnection repo = new PmrepConnection
            {
                domain = "Domain_melchior",
                repository = "infa_rep_melchior",
                userName = "Administrator",
                password = "k2kb2bdbgv"
            };


        [TestMethod]
        public void PmrepConnectionTest()
        {
            Pmrep pmrep = new Pmrep(@"C:\pmrep.bat",null);

            Assert.AreEqual(true, true);
        }
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException),
        "File not found!")]
        public void PmrepFailedIsFileNotFound()
        {
           // Pmrep pmrep = new Pmrep(@"C:\pmrep.ba", null);
        }

       
    }
}
