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
            var dictParam = new Dictionary<char, string>();
            dictParam.Add('r', "test reponame");
            dictParam.Add('h', "test host");
            Pmrep pmrep = new Pmrep(@"C:\pmrep.bat",dictParam);

            Assert.AreEqual(true, true);
        }
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException),
        "File not found!")]
        public void PmrepFailedIsFileNotFound()
        {
            Pmrep pmrep = new Pmrep(@"C:\pmrep.ba", null);
        }

        [TestMethod]
        public void PmrepLogFileIsHave()
        {
            var dictParam = new Dictionary<char, string>();
            dictParam.Add('r', "test reponame");
            dictParam.Add('h', "test host");
            Pmrep pmrep = new Pmrep(@"C:\pmrep.bat", dictParam,@"C:\pmrep.log");

            Assert.IsTrue(File.Exists(@"C:\pmrep.log"));
        }

        [TestMethod]
        public void PmrepListConnectTest()
        {
            var dictParam = new Dictionary<char, string>();
            dictParam.Add('r', "test reponame");
            dictParam.Add('h', "test host");
            Pmrep pmrep = new Pmrep(@"C:\pmrep.bat", dictParam, @"C:\pmrep1.log");
            Assert.IsTrue(pmrep.ListConnections().Length > 0);
        }
    }
}
