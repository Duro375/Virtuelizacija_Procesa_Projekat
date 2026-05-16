using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TextManipulation : IDisposable
    {
        private TextWriter textWriter;
        private TextReader textReader;
        private bool disposed = false;
        string path = "";
        public string Path { get => path; }

        public TextManipulation(string path)
        {
            this.path = path;
        }

        ~TextManipulation()
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
                    if (textWriter != null)
                    {
                        textWriter.Dispose();
                    }
                    if (textReader != null)
                    {
                        textReader.Dispose();
                    }
                }
                disposed = true;
            }
        }

        public void AddTextToFile(string text)
        {
            textWriter = File.AppendText(path);
            textWriter.WriteLine(text);
            textWriter.Close();
        }

        public string ReadAllText()
        {
            textReader = File.OpenText(path);
            string text = textReader.ReadToEnd();
            textReader.Close();
            return text;
        }

        public void DeleteAllText()
        {
            File.WriteAllText(path, string.Empty);
        }
    }
}
