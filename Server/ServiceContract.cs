using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Server
{
    public class ServiceContract : IServiceContract
    {
        public void StartSession()
        {
        }

        public void PushSample(DataContract data)
        {
            string error = Validate(data);
            if (error != null)
            {
                SendFaultMessage(error);
            }
        }
        public void EndSession()
        {
        }

        //3. zadatak: Validacija i fault poruka
        private string Validate(DataContract data)
        {
            if(data.TimeStamp <= DateTime.MinValue)
                return "Invalid Timestapm";
            if(data.Voltage_RMS_Avg < 0)
                return string.Format("Nevalidan podatak Voltage_RMS_Avg: {0}", data.Voltage_RMS_Avg);
            if (data.Current_RMS_Avg < 0)
                return string.Format("Nevalidan podatak Current_RMS_Avg: {0}", data.Current_RMS_Avg); 
            if(data.Real_Power_Avg < 0)
                return string.Format("Nevalidan podatak Real_Power_Avg: {0}", data.Real_Power_Avg); 
            if (data.Reactive_Power_Avg > 0)
                return string.Format("Nevalidan podatak Reactive_Power_Avg: {0}", data.Reactive_Power_Avg);
            if (data.Apparent_Power_Avg < 0)
                return string.Format("Nevalidan podatak Apparent_Power_Avg: {0}", data.Apparent_Power_Avg);
            if (data.Frequency_Avg < 0)
                return string.Format("Nevalidan podatak Frequency_Avg: {0}", data.Frequency_Avg);
            return null;
        }

        private void SendFaultMessage(string message)
        {
            throw new FaultException<CustomException>(
                new CustomException(message),
                new FaultReason(message));
        }
    }
}
