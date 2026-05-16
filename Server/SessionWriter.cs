using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Server
{
    public class SessionWriter : IDisposable
    {
        private FileStream _sessionStream;
        private StreamWriter _sessionWriter;


        public void WriteRow(DataContract data)
        {

        }
        public void Dispose()
        {
            if (_sessionStream == null && _sessionWriter == null) return;
            if(_sessionWriter != null)
            {
                try
                {
                    _sessionWriter.Dispose();
                    _sessionWriter.Close();
                    _sessionWriter = null;
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("Unsuccesful disposing of session writer!");
                }
            }
            if( _sessionStream != null )
            {
                try
                {
                    _sessionStream.Dispose();
                    _sessionStream.Close();
                    _sessionStream = null;

                }
                catch (Exception)
                {
                    Console.WriteLine("Unsuccesful disposing of session stream!");
                }
            }
        }
    }
}
