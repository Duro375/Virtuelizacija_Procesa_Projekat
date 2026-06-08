using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CustomEventArgs : EventArgs
    {
        public int VehicleId { get; set; }
        public int RowIndex { get; set; }
        public string Message { get; set; }

        public CustomEventArgs() { }

        public CustomEventArgs(int vehicleId, int rowIndex, string message)
        {
            VehicleId = vehicleId;
            RowIndex = rowIndex;
            Message = message;
        }
    }
}
