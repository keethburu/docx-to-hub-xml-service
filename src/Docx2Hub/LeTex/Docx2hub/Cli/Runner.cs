using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
namespace Docx2HubSvc.LeTex.Docx2hub.Cli
{
   

   
   
    
   
    public class Runner
    {

        public static async Task<ConversionResult> Docx2HubAsync(string inputDocxPath)
        {
            if (!File.Exists(inputDocxPath)) {
                throw new FileNotFoundException($"{nameof(inputDocxPath)} does not exists");
            }
            ConversionResult res = null;
            var targetDir = Path.GetDirectoryName(inputDocxPath);

            var stdOutBuffer = new StringBuilder();
            var stdErrBuffer = new StringBuilder();

            var script = "/opt/docx2hub/docx2hub.sh";
            var cmd = CliWrap.Cli.Wrap("/bin/bash")
                       .WithArguments($"-c \"{script} -o {targetDir} '{inputDocxPath}'\"")
                        .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
                       .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
                       .WithValidation(CommandResultValidation.None);
            var expectedLogPath = Path.Combine( Path.GetDirectoryName(inputDocxPath), Path.GetFileNameWithoutExtension(inputDocxPath) + ".log");
            var expectedResultFilePath = Path.Combine( Path.GetDirectoryName(inputDocxPath), Path.GetFileNameWithoutExtension(inputDocxPath) + ".xml");
            try {
                var cmdRes = await cmd.ExecuteAsync();
                res = new ConversionResult() {
                    StdErr = stdErrBuffer.ToString(),
                    StdOut = stdOutBuffer.ToString(),
                    ExitCode = cmdRes.ExitCode,
                    StartTime = cmdRes.StartTime,
                    ExitTime = cmdRes.ExitTime,
                    RunTime = cmdRes.RunTime,
                    Log = ReadLog(expectedLogPath),
                    ResultFilePath = File.Exists(expectedResultFilePath) ? expectedResultFilePath : null
                };
            } 
            catch (Exception ex) {
                res = new ConversionResult() {
                    StdErr = stdErrBuffer.ToString(),
                    StdOut = stdOutBuffer.ToString(),
                    Message = ex.Message,
                    Log = ReadLog(expectedLogPath),
                    ResultFilePath = File.Exists(expectedResultFilePath) ? expectedResultFilePath : null
                };
            }
            return res;
        }

        private static string ReadLog(string expectedLogPath) {
            if (File.Exists(expectedLogPath)) {
                return File.ReadAllText(expectedLogPath);
            }
            return null;
            
        }
       
    }
}
