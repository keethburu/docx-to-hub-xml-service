using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using KeethBuru.Util;
using System.Text;
using CliWrap;
using MimeTypes;
using Docx2HubSvc.LeTex.Docx2hub.Cli;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Docx2HubSvc.Controllers
{



    //[Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    //[ApiVersion("1.0")]
    //[ApiController]

    [ApiController]
    [Route("[controller]")]
    public partial class FileController : ControllerBase
    {

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            try
            {

                if (file.Length > 0)
                {
                    var tempDir = Path.GetTempPath();

                    var requestId = Guid.NewGuid().ToString();

                    var tempFileDir = Path.Combine(tempDir, requestId);
                    var tempDirInf = new DirectoryInfo(tempFileDir);
                    tempDirInf.Create();
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var normalizedFileName = requestId + ".docx";
                    var tempFilePath = Path.Combine(tempFileDir, normalizedFileName);

                    ConversionResult cmdRes = null;

                    using (var tempFileStream = new FileStream(tempFilePath, mode: FileMode.Create)) {
                        file.CopyTo(tempFileStream);
                        tempFileStream.Close();
                    }

                    cmdRes = await Runner.Docx2HubAsync(tempFilePath);

                    if (cmdRes.ResultFilePath == null || System.IO.File.Exists(cmdRes.ResultFilePath) != true) {
                        string details = cmdRes.ToString();
                        return Problem(detail:details);
                    }

                    return PhysicalFile(cmdRes.ResultFilePath,MimeTypeMap.GetMimeType("xml"));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            
           
        }

       
    }
}
