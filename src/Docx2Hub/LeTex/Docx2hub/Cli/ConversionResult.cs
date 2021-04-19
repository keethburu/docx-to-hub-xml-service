using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Docx2HubSvc.LeTex.Docx2hub.Cli
{
 
        public record ConversionResult
        {
            public string StdOut { get; init; }
            public string StdErr { get; init; }

            public string ResultFilePath { get; init; }

            public string Log {get; init;}

            public string Message {get; init; }
            public int ExitCode { get; init; }
            public DateTimeOffset StartTime { get; init; }
            public TimeSpan RunTime { get; init; }
            public DateTimeOffset ExitTime { get; init; }
        }

}
