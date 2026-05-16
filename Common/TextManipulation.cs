using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Common
{
    public class TextManipulation : IDisposable
    {
        private FileStream stream;
        private StreamReader reader;
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
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
                disposed = true;
            }
        }

        public void Initialize()
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            reader = new StreamReader(stream);
        }

        public string ReadLine()
        {
            if (reader == null)
            {
                throw new Exception("Reader nije inicijalizovan!");
            }
            return reader.ReadLine();
        }

        public DataContract ConvertToData(string line, int id, int row)
        {
            try
            {
                string[] deo = line.Split(',');
                DateTime timestamp = DateTime.ParseExact(deo[0], "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                double vMin = double.Parse(deo[1], CultureInfo.InvariantCulture);
                double vAvg = double.Parse(deo[2], CultureInfo.InvariantCulture);
                double vMax = double.Parse(deo[3], CultureInfo.InvariantCulture);
                double aMin = double.Parse(deo[4], CultureInfo.InvariantCulture);
                double aAvg = double.Parse(deo[5], CultureInfo.InvariantCulture);
                double aMax = double.Parse(deo[6], CultureInfo.InvariantCulture);
                double kWMin = double.Parse(deo[7], CultureInfo.InvariantCulture);
                double kWAvg = double.Parse(deo[8], CultureInfo.InvariantCulture);
                double kWMax = double.Parse(deo[9], CultureInfo.InvariantCulture);
                double kVARMin = double.Parse(deo[10], CultureInfo.InvariantCulture);
                double kVARAvg = double.Parse(deo[11], CultureInfo.InvariantCulture);
                double kVARMax = double.Parse(deo[12], CultureInfo.InvariantCulture);
                double kVAmin = double.Parse(deo[13], CultureInfo.InvariantCulture);
                double kVAavg = double.Parse(deo[14], CultureInfo.InvariantCulture);
                double kVAmax = double.Parse(deo[15], CultureInfo.InvariantCulture);
                double fMin = double.Parse(deo[16], CultureInfo.InvariantCulture);
                double fAvg = double.Parse(deo[17], CultureInfo.InvariantCulture);
                double fMax = double.Parse(deo[18], CultureInfo.InvariantCulture);
                DataContract data = new DataContract(id, row, timestamp, vMin, vAvg, vMax, aMin, aAvg, aMax, kWMin, kWAvg, kWMax, kVARMin, kVARAvg, kVARMax, kVAmin, kVAavg, kVAmax, fMin, fAvg, fMax);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Validate(DataContract data)
        {
            if (data.Voltage_RMS_Min <= 0 || data.Voltage_RMS_Avg <= 0 || data.Voltage_RMS_Max <= 0 || data.Current_RMS_Min <= 0 || data.Current_RMS_Avg <= 0 || data.Current_RMS_Max <= 0 || data.Real_Power_Min <= 0 || data.Real_Power_Avg <= 0 || data.Real_Power_Max <= 0 || data.Apparent_Power_Min <= 0 || data.Apparent_Power_Avg <= 0 || data.Apparent_Power_Max <= 0 || data.Frequency_Min <= 0 || data.Frequency_Avg <= 0 || data.Frequency_Max <= 0)
            {
                return false;
            }
            return true;
        }

        public int GetNumberOfLines()
        {
            if (reader == null)
            {
                throw new Exception("Reader nije inicijalizovan!");
            }
            int count = 0;
            while (reader.ReadLine() != null)
            {
                count++;
            }
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            reader.DiscardBufferedData();
            return count;
        }
    }
}
