using System;
using System.IO;

namespace KeethBuru.Util
{
    public class TempFileStream : FileStream
    {
        static private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public const int DEFAULT_BUFFER_SIZE = 32768;
        public TempFileStream(string path = null,
            int bufferSize = DEFAULT_BUFFER_SIZE,
            FileMode mode = FileMode.OpenOrCreate,
            FileAccess access = FileAccess.ReadWrite,
            FileShare share = FileShare.Read,
            FileOptions options = FileOptions.RandomAccess)
            : base(path, mode, access, share, bufferSize: bufferSize, options: options) 
            {
             Path = path ?? System.IO.Path.GetTempFileName();
            }
        public string Path { get; private set; }

        public bool DeleteOnDisposal { get; set; } = true;

        private bool disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    base.Dispose(disposing);
                    if (DeleteOnDisposal && !(Path is null) && File.Exists(Path))
                    {
                        try
                        {
                            File.Delete(Path);
                        }
                        catch (Exception ex)
                        {
                            _logger.Fatal(ex, "failed deleting temp file");
                        }
                    }
                }
                Path = null;
                disposedValue = true;
            }
        }
    }
}