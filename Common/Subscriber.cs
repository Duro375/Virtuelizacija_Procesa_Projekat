using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Subscriber
    {
        public void HandleTransferMessage(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"[INFO]: Vozilo {e.VehicleId} -> {e.Message}");
        }

        public void HandleWarningMessage(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"[WARNING]: Greska u redu {e.RowIndex} -> {e.Message}");
        }

        public void HandleSampleMessage(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"[SAMPLE]: Pristigao je red {e.RowIndex} -> {e.Message}");
        }
    }
}
