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

        public SessionWriter(string filePath)
        {
            _sessionStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            _sessionWriter = new StreamWriter(_sessionStream);
        }

        public void WriteRow(DataContract data)
        {
            if (_sessionWriter == null)
            {
                throw new ObjectDisposedException(nameof(SessionWriter), "Pokusaj upisa u stream koji je zatvoren.");
            }

            string row = $"{data.RowIndex},{data.TimeStamp:yyyy-MM-dd HH:mm:ss.fff}," +
                         $"{data.Voltage_RMS_Min},{data.Voltage_RMS_Avg},{data.Voltage_RMS_Max}" +
                         $"{data.Current_RMS_Min},{data.Current_RMS_Avg},{data.Current_RMS_Max}" +
                         $"{data.Real_Power_Min},{data.Real_Power_Avg},{data.Real_Power_Max}" +
                         $"{data.Reactive_Power_Min},{data.Reactive_Power_Avg},{data.Reactive_Power_Max}"+
                         $"{data.Apparent_Power_Min},{data.Apparent_Power_Avg},{data.Apparent_Power_Max}"+
                         $"{data.Frequency_Min},{data.Frequency_Avg},{data.Frequency_Max}";

            _sessionWriter.WriteLine(row);
            _sessionWriter.Flush();
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
                    Console.WriteLine("Dispose session writer-a je neuspesan!");
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
                    Console.WriteLine("Dispose session stream-a je neuspesan!");
                }
            }
        }
    }
}
