using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class MeasuredValue
    {
        [DataMember]
        public double MinValue { get; set; }
        public double AvgValue { get; set; }
        public double MaxValue { get; set; }
        public MeasuredValue() { }
        public MeasuredValue(double minValue, double avgValue, double maxValue)
        {
            MinValue = minValue;
            AvgValue = avgValue;
            MaxValue = maxValue;
        }
    }
}
