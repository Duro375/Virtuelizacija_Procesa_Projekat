using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class DataContract
    {
        [DataMember]
        public int VehicleId { get; set; }
        [DataMember]
        public int RowIndex { get; set; }
        [DataMember]
        public DateTime TimeStamp { get; set; }
        [DataMember]
        public double Voltage_RMS_Min { get; set; }
        [DataMember]
        public double Voltage_RMS_Max { get; set; }
        [DataMember]
        public double Voltage_RMS_Avg { get; set; }
        [DataMember]
        public double Current_RMS_Min { get; set; }
        [DataMember]
        public double Current_RMS_Max { get; set; }
        [DataMember]
        public double Current_RMS_Avg { get; set; }
        [DataMember]
        public double Real_Power_Min { get; set; }
        [DataMember]
        public double Real_Power_Avg { get; set; }
        [DataMember]
        public double Real_Power_Max { get; set; }
        [DataMember]
        public double Reactive_Power_Min { get; set; }
        [DataMember]
        public double Reactive_Power_Avg { get; set; }
        [DataMember]
        public double Reactive_Power_Max { get; set; }
        [DataMember]
        public double Apparent_Power_Min { get; set; }
        [DataMember]
        public double Apparent_Power_Avg { get; set; }
        [DataMember]
        public double Apparent_Power_Max { get; set; }
        [DataMember]
        public double Frequency_Min { get; set; }
        [DataMember]
        public double Frequency_Avg { get; set; }
        [DataMember]
        public double Frequency_Max { get; set; }

        public DataContract(int vehicleId, int rowIndex, DateTime timeStamp, double voltage_RMS_Min, double voltage_RMS_Max, double voltage_RMS_Avg, double current_RMS_Min, double current_RMS_Max, double current_RMS_Avg, double real_Power_Min, double real_Power_Avg, double real_Power_Max, double reactive_Power_Min, double reactive_Power_Avg, double reactive_Power_Max, double apparent_Power_Min, double apparent_Power_Avg, double apparent_Power_Max, double frequency_Min, double frequency_Avg, double frequency_Max)
        {
            VehicleId = vehicleId;
            RowIndex = rowIndex;
            TimeStamp = timeStamp;
            Voltage_RMS_Min = voltage_RMS_Min;
            Voltage_RMS_Max = voltage_RMS_Max;
            Voltage_RMS_Avg = voltage_RMS_Avg;
            Current_RMS_Min = current_RMS_Min;
            Current_RMS_Max = current_RMS_Max;
            Current_RMS_Avg = current_RMS_Avg;
            Real_Power_Min = real_Power_Min;
            Real_Power_Avg = real_Power_Avg;
            Real_Power_Max = real_Power_Max;
            Reactive_Power_Min = reactive_Power_Min;
            Reactive_Power_Avg = reactive_Power_Avg;
            Reactive_Power_Max = reactive_Power_Max;
            Apparent_Power_Min = apparent_Power_Min;
            Apparent_Power_Avg = apparent_Power_Avg;
            Apparent_Power_Max = apparent_Power_Max;
            Frequency_Min = frequency_Min;
            Frequency_Avg = frequency_Avg;
            Frequency_Max = frequency_Max;
        }
    }
}
