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
using System.Text.RegularExpressions;

namespace Docx2HubSvcTest
{
     [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void  GetRequestIdandTempDirTest() {
             (string requestId, string tempFileDir) = Helper.GetRequestIdandTempDir();
             Assert.IsTrue(Directory.Exists(tempFileDir));
            Directory.Delete(tempFileDir);
            Regex requestIdRegex = new Regex(@"^([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$",RegexOptions.IgnoreCase);
            Assert.IsTrue(requestIdRegex.IsMatch(requestId));
            Regex pathRegex = new Regex(@"/tmp/Docx2HubSvc/\d{4}-\d{2}-\d{2}_\d{2}/"+requestId,RegexOptions.IgnoreCase);
            Assert.IsTrue(pathRegex.IsMatch(tempFileDir));
        }

    }
}