using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Test()
        {
            bool DirectoryExists = Directory.Exists(Server.Service.DirName);
            bool FileExists = File.Exists(Path.Combine(Server.Service.DirName, "TestFile.txt"));

            
            Assert.IsTrue(DirectoryExists);
            Assert.IsTrue(FileExists);
        }
    }
}
