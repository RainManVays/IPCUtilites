using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpcPmrep;
using System.Collections.Generic;
using System.IO;

namespace UnitTestIPCPmrep
{
    [TestClass]
    public class UnitTestIPCPmrep
    {
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

        [TestMethod]
        public void PmrepLogFileIsHave()
        {
            Pmrep pmrep = new Pmrep(@"C:\pmrep.bat", null,@"C:\pmrep.log");

            Assert.IsTrue(File.Exists(@"C:\pmrep.log"));
        }

        [TestMethod]
        public void PmrepListConnectTest()
        {
            Pmrep pmrep = new Pmrep(@"C:\pmrep.bat", null, @"C:\pmrep1.log");
            Assert.IsTrue(pmrep.ListConnections().Length > 0);
        }
        [TestMethod]
        public void TestPmrepConnections()
        {
            var repo = new PmrepConnection { domain = "melchior", port = "6005" };
            Assert.Equals(repo.domain, "-d melchior");
            Assert.Equals(repo.hostName, "");
        }
    }
}
