using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Logger : ILogger, IDisposable
    {
        private FileStream stream;
        private StreamWriter writer;
        private bool disposed = false;
        string path = "";
        public string Path { get => path; }

        public Logger(string path)
        {
            this.path = path;
            
        }
        public void Log(string message, LogType type)
        {
            string logMessage = $"[{DateTime.Now}] [{type}] {message}";
            stream = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (writer = new StreamWriter(stream))
            {
                writer.WriteLine(logMessage);
            }
            stream.Close();
            writer.Close();
        }

        ~Logger()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                    if (writer != null)
                    {
                        writer.Dispose();
                    }
                }
                disposed = true;
            }
        }
    }
}
