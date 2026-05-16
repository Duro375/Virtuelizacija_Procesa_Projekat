using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Server
{
    public class ServiceContract : IServiceContract
    {
        public string EndSession()
        {
            throw new NotImplementedException();
        }

        public string PushSample(DataContract data)
        {
            throw new NotImplementedException();
        }

        public string StartSession()
        {
            throw new NotImplementedException();
        }

        private bool Validate(DataContract data)
        {
            if(data.TimeStamp <= DateTime.MinValue)
                return false;
            if(data.Voltage_RMS_Avg < 0)
                return false;
            if(data.Current_RMS_Avg < 0)
                return false;
            if(data.Real_Power_Avg < 0)
                return false;
            if(data.Reactive_Power_Avg < 0)
                return false;
            if(data.Apparent_Power_Avg < 0)
                return false;
            if(data.Frequency_Avg < 0)
                return false;
            return true;
        }
    }
}
