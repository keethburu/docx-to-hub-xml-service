using System;
using System.IO;


namespace Docx2HubSvc.Util {
    public class Helper {

        /// <summary>
        /// create a request ID (an GUID string), and a tempt dir, and return the ID and the temp dir,
        /// the temp dir will be smth like /tmp/Docx2HubSvc/{timestamp}/{GUID}
        /// the timestamp will be formatted yyyy-MM-dd_HH, and the GUID is created using <see cref="Guid.ToString()"/>
        /// </summary>
        public static (string requestId,string tempDirPath) GetRequestIdandTempDir() {
            var tempDir = Path.GetTempPath();
            var requestId = Guid.NewGuid().ToString();
            var timeStampHr = DateTime.Now.ToString("yyyy-MM-dd_HH");
            var tempFileDir = Path.Combine(tempDir,"Docx2HubSvc",timeStampHr ,requestId);
            var tempDirInf = new DirectoryInfo(tempFileDir);
            tempDirInf.Create();
            return (requestId,tempFileDir);
        }
    }
}