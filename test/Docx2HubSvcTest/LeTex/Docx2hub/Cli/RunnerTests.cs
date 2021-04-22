using System.Diagnostics;
using System;
using System.IO;
using System.Threading.Tasks;
using Docx2HubSvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
using Moq;
using Docx2HubSvc.LeTex.Docx2hub.Cli;
using Docx2HubSvc.Util;

namespace Docx2HubSvcTest
{
     [TestClass]
    public class RunnerTests
    {
        [TestMethod]
        public async Task Docx2HubAsyncTest() {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var testDocx = Path.Combine(baseDir, @"Files", @"test.docx");
            Assert.IsTrue(File.Exists(testDocx),$"{testDocx} does not exist");

            (string requestId, string tempFileDir) = Helper.GetRequestIdandTempDir();
            var tempFilePath = Path.Combine(tempFileDir,Path.GetFileName(testDocx));
            File.Copy(testDocx,tempFilePath);

            ConversionResult cmdRes = null;
            cmdRes = await Runner.Docx2HubAsync(tempFilePath);
            var fileExists = File.Exists(cmdRes.ResultFilePath);
            Directory.Delete(tempFileDir,true);
            Assert.AreEqual(0,cmdRes.ExitCode );
            
            Assert.IsTrue(fileExists);

        }

    }
}