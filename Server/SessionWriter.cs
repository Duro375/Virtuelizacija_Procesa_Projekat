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
                         $"{data.Voltage.MinValue},{data.Voltage.AvgValue},{data.Voltage.MaxValue}," +
                         $"{data.Current_RMS.MinValue},{data.Current_RMS.AvgValue},{data.Current_RMS.MaxValue}," +
                         $"{data.Real_Power.MinValue},{data.Real_Power.AvgValue},{data.Real_Power.MaxValue}," +
                         $"{data.Reactive_Power.MinValue},{data.Reactive_Power.AvgValue},{data.Reactive_Power.MaxValue},"+
                         $"{data.Apparent_Power.MinValue},{data.Apparent_Power.AvgValue},{data.Apparent_Power.MaxValue},"+
                         $"{data.Frequency.MinValue},{data.Frequency.AvgValue},{data.Frequency.MaxValue}";

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

        public void FirstRow()
        {
            if (_sessionWriter == null)
            {
                throw new ObjectDisposedException(nameof(SessionWriter), "Pokusaj upisa u stream koji je zatvoren.");
            }

            string row = "Row Index,Date Time,Voltage RMS Min (V),Voltage RMS Avg (V),Voltage RMS Max (V),Current RMS Min (A),Current RMS Avg (A),Current RMS Max (A),Real Power Min (kW),Real Power Avg (kW),Real Power Max (kW),Reactive Power Min (kVAR),Reactive Power Avg (kVAR),Reactive Power Max (kVAR),Apparent Power Min (kVA),Apparent Power Avg (kVA),Apparent Power Max (kVA),Frequency Min (Hz),Frequency Avg (Hz),Frequency Max (Hz)";

            _sessionWriter.WriteLine(row);
            _sessionWriter.Flush();
        }
    }
}
