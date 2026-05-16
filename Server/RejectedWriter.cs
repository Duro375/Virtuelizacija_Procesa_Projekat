using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Server
{
    public class RejectedWriter : IDisposable
    {
        private FileStream _rejectionStream;
        private StreamWriter _rejectionWriter;


        public void WriteRejection(DataContract data)
        {

        }
        public void Dispose()
        {
            if (_rejectionStream == null && _rejectionWriter == null) return;
            if (_rejectionWriter != null)
            {
                try
                {
                    _rejectionWriter.Dispose();
                    _rejectionWriter.Close();
                    _rejectionWriter = null;

                }
                catch (Exception)
                {
                    Console.WriteLine("Unsuccesful disposing of rejection writer!");
                }
            }
            if (_rejectionStream != null)
            {
                try
                {
                    _rejectionStream.Dispose();
                    _rejectionStream.Close();
                    _rejectionStream = null;

                }
                catch (Exception)
                {
                    Console.WriteLine("Unsuccesful disposing of rejection stream!");
                }
            }
        }
    }
}
