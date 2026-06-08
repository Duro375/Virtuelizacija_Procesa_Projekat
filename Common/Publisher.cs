using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Publisher
    {
        public delegate void PublisherEventHandler(object sender, CustomEventArgs e);
        public event PublisherEventHandler OnTransferStarted;
        public event PublisherEventHandler OnSampleRecieved;
        public event PublisherEventHandler OnTransferCompleted;
        public event PublisherEventHandler OnWarningRaised;

        public void Handle(string type, int vehicleId, int rowIndex, string message)
        {
            CustomEventArgs args = new CustomEventArgs(vehicleId, rowIndex, message);
            switch (type)
            {
                case "start":
                    OnTransferStarted?.Invoke(this, args);
                    break;
                case "sample":
                    OnSampleRecieved?.Invoke(this, args);
                    break;
                case "end":
                    OnTransferCompleted?.Invoke(this, args);
                    break;
                case "warning":
                    OnWarningRaised?.Invoke(this, args);
                    break;
            }
        }
    }
}
