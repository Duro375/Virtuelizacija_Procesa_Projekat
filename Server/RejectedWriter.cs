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


        public RejectedWriter(string filePath)
        {
            _rejectionStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            _rejectionWriter = new StreamWriter(_rejectionStream);
        }
        public void WriteRejection(DataContract data)
        {
            if (_rejectionWriter == null)
            {
                throw new ObjectDisposedException(nameof(SessionWriter), "Pokusaj upisa u stream koji je zatvoren.");
            }

            string row = $"{data.RowIndex},{data.TimeStamp:yyyy-MM-dd HH:mm:ss.fff}," +
                         $"{data.Voltage.MinValue},{data.Voltage.AvgValue},{data.Voltage.MaxValue}," +
                         $"{data.Current_RMS.MinValue},{data.Current_RMS.AvgValue},{data.Current_RMS.MaxValue}," +
                         $"{data.Real_Power.MinValue},{data.Real_Power.AvgValue},{data.Real_Power.MaxValue}," +
                         $"{data.Reactive_Power.MinValue},{data.Reactive_Power.AvgValue},{data.Reactive_Power.MaxValue}," +
                         $"{data.Apparent_Power.MinValue},{data.Apparent_Power.AvgValue},{data.Apparent_Power.MaxValue}," +
                         $"{data.Frequency.MinValue},{data.Frequency.AvgValue},{data.Frequency.MaxValue}";

            _rejectionWriter.WriteLine(row);
            _rejectionWriter.Flush();
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
                    Console.WriteLine("Dispose rejection writer-a je neuspesan!");
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
                    Console.WriteLine("Dispose rejection stream-a je neuspesan!");
                }
            }
        }

        public void FirstRow()
        {
            if (_rejectionWriter == null)
            {
                throw new ObjectDisposedException(nameof(SessionWriter), "Pokusaj upisa u stream koji je zatvoren.");
            }

            string row = "Row Index,Date Time,Voltage RMS Min (V),Voltage RMS Avg (V),Voltage RMS Max (V),Current RMS Min (A),Current RMS Avg (A),Current RMS Max (A),Real Power Min (kW),Real Power Avg (kW),Real Power Max (kW),Reactive Power Min (kVAR),Reactive Power Avg (kVAR),Reactive Power Max (kVAR),Apparent Power Min (kVA),Apparent Power Avg (kVA),Apparent Power Max (kVA),Frequency Min (Hz),Frequency Avg (Hz),Frequency Max (Hz)";

            _rejectionWriter.WriteLine(row);
            _rejectionWriter.Flush();
        }
    }
}
