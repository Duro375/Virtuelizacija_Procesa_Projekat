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
        public MeasuredValue Voltage { get; set; }
        [DataMember]
        public MeasuredValue Current_RMS { get; set; }
        [DataMember]
        public MeasuredValue Real_Power { get; set; }
        [DataMember]
        public MeasuredValue Reactive_Power { get; set; }
        [DataMember]
        public MeasuredValue Apparent_Power { get; set; }
        [DataMember]
        public MeasuredValue Frequency { get; set; }

        public DataContract(int vehicleId, int rowIndex, DateTime timeStamp, double voltage_RMS_Min, double voltage_RMS_Avg, double voltage_RMS_Max, double current_RMS_Min, double current_RMS_Avg, double current_RMS_Max, double real_Power_Min, double real_Power_Avg, double real_Power_Max, double reactive_Power_Min, double reactive_Power_Avg, double reactive_Power_Max, double apparent_Power_Min, double apparent_Power_Avg, double apparent_Power_Max, double frequency_Min, double frequency_Avg, double frequency_Max)
        {
            VehicleId = vehicleId;
            RowIndex = rowIndex;
            TimeStamp = timeStamp;
            Voltage = new MeasuredValue(voltage_RMS_Min, voltage_RMS_Avg, voltage_RMS_Max);
            Current_RMS = new MeasuredValue(current_RMS_Min, current_RMS_Avg, current_RMS_Max);
            Real_Power = new MeasuredValue(real_Power_Min, real_Power_Avg, real_Power_Max);
            Reactive_Power = new MeasuredValue(reactive_Power_Min, reactive_Power_Avg, reactive_Power_Max);
            Apparent_Power = new MeasuredValue(apparent_Power_Min, apparent_Power_Avg, apparent_Power_Max);
            Frequency = new MeasuredValue(frequency_Min, frequency_Avg, frequency_Max);
        }

        public bool IsValid()
        {
            return Voltage.MinValue > 0 && Voltage.AvgValue > 0 && Voltage.MaxValue > 0 &&
                   Current_RMS.MinValue > 0 && Current_RMS.AvgValue > 0 && Current_RMS.MaxValue > 0 &&
                   Real_Power.MinValue > 0 && Real_Power.AvgValue > 0 && Real_Power.MaxValue > 0 &&
                   Apparent_Power.MinValue > 0 && Apparent_Power.AvgValue > 0 && Apparent_Power.MaxValue > 0 &&
                   Frequency.MinValue > 0 && Frequency.AvgValue > 0 && Frequency.MaxValue > 0 &&
                   Reactive_Power.MinValue < 0 && Reactive_Power.AvgValue < 0 && Reactive_Power.MaxValue < 0;
        }
    }
}
