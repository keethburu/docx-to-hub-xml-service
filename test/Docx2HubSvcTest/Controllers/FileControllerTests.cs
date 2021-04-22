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

namespace Docx2HubSvcTest
{
    [TestClass]
    public class FileControllerTests
    {

        [TestMethod]
        public void Docx2HubExistsTest() {
            var path = "/opt/docx2hub/docx2hub.sh";
            Assert.IsTrue(System.IO.File.Exists(path),$"{path} does not exist");
        }

        [TestMethod]
        public async Task UploadAsyncTest()
        {

            var fileMock = new Mock<IFormFile>();
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var testDocx = Path.Combine(baseDir, @"Files", @"test.docx");
            var fileName = "testDoc.docx";
            Assert.IsTrue(File.Exists(testDocx),$"{testDocx} does not exist");
            using var fs = new FileStream(testDocx, FileMode.Open, FileAccess.Read);
            using var ms = new MemoryStream();
            fs.CopyTo(ms);
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.ContentDisposition).Returns($"form-data; name=\"file\"; filename=\"{fileName}\"");
            fileMock.Setup(_ => _.CopyTo(It.IsAny<Stream>())).Callback<Stream>((s) => {
                ms.CopyTo(s);
            });
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            var serviceUnderTest = new FileController();
            var file = fileMock.Object;

   
            var result = await serviceUnderTest.UploadAsync(file);

            string resultString = JsonSerializer.Serialize(result as Microsoft.AspNetCore.Mvc.ObjectResult);
            Assert.IsInstanceOfType(result, typeof(PhysicalFileResult),$"test failed {resultString}");
            
        }
    }
}
