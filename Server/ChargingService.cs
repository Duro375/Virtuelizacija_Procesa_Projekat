using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChargingService : IChargingService
    {
        public RejectedWriter rejects;
        public SessionWriter session;

        private readonly Publisher publisher;
        public ChargingService(Publisher publisher)
        {
            this.publisher = publisher;
        }

        public void StartSession(int vehicleId)
        {
            string path = "Data";
            string sessionPath = "";
            string rejectsPath = "";

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += "/" + vehicleId;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += "/" + DateTime.Now.ToString("yyyy-MM-dd");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            sessionPath = path + "/session.csv";
            rejectsPath = path + "/rejects.csv";

            File.Create(sessionPath).Close();
            File.Create(rejectsPath).Close();

            session = new SessionWriter(sessionPath);
            rejects = new RejectedWriter(rejectsPath);

            publisher.Handle("start", vehicleId, 0, "Prenos je zapoceo...");

            session.FirstRow();
            rejects.FirstRow();
        }

        public void PushSample(DataContract data)
        {
            {
                string error = Validate(data);
                if (error != null)
                {
                    try
                    {
                        rejects.WriteRejection(data);
                        publisher.Handle("sample", data.VehicleId, data.RowIndex, error);
                    }
                    catch (Exception)
                    {

                    }

                    SendFaultMessage(error);
                }
                else
                {
                    if (session == null)
                    {
                        SendFaultMessage("Session is not started.");
                    }
                    else
                    {
                        session.WriteRow(data);
                        publisher.Handle("sample", data.VehicleId, data.RowIndex, "Podatak je uspesno primljen.");
                    }
                }

            }

        }
        public void EndSession(int vehicleId)
        {
            session.Dispose();
            rejects.Dispose();
            publisher.Handle("end", vehicleId, 0, "Prenos je zavrsen");
        }

        //3. zadatak: Validacija i fault poruka
        private string Validate(DataContract data)
        {
            if(data.TimeStamp <= DateTime.MinValue)
                return "Invalid Timestapm";
            if(data.Voltage.AvgValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Voltage_RMS_Avg: {1}", data.RowIndex, data.Voltage.AvgValue);
            if (data.Voltage.MinValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Voltage_RMS_Min: {1}", data.RowIndex, data.Voltage.MinValue);
            if (data.Voltage.MaxValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Voltage_RMS_Max: {1}", data.RowIndex, data.Voltage.MaxValue);

            if (data.Current_RMS.AvgValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Current_RMS_Avg: {1}",data.RowIndex, data.Current_RMS.AvgValue);
            if (data.Current_RMS.MinValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Current_RMS_Min: {1}", data.RowIndex, data.Current_RMS.MinValue);
            if (data.Current_RMS.MaxValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Current_RMS_Max: {1}", data.RowIndex, data.Current_RMS.MaxValue);

            if (data.Real_Power.AvgValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Real_Power_Avg: {1}", data.RowIndex, data.Real_Power.AvgValue);
            if (data.Real_Power.MinValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Real_Power_Min: {1}", data.RowIndex, data.Real_Power.MinValue);
            if (data.Real_Power.MaxValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Real_Power_Max: {1}", data.RowIndex, data.Real_Power.MaxValue);

            if (data.Reactive_Power.AvgValue >= 0)
                return string.Format("Nevalidan podatak Row:{0} Reactive_Power_Avg: {1}", data.RowIndex, data.Reactive_Power.AvgValue);
            if (data.Reactive_Power.MinValue >= 0)
                return string.Format("Nevalidan podatak Row:{0} Reactive_Power_Min: {1}", data.RowIndex, data.Reactive_Power.MinValue);
            if (data.Reactive_Power.MaxValue >= 0)
                return string.Format("Nevalidan podatak Row:{0} Reactive_Power_Max: {1}", data.RowIndex, data.Reactive_Power.MaxValue);

            if (data.Apparent_Power.AvgValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Apparent_Power_Avg: {1}", data.RowIndex, data.Apparent_Power.AvgValue);
            if (data.Apparent_Power.MinValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Apparent_Power_Min: {1}", data.RowIndex, data.Apparent_Power.MinValue);
            if (data.Apparent_Power.MaxValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Apparent_Power_Max: {1}", data.RowIndex, data.Apparent_Power.MaxValue);


            if (data.Frequency.AvgValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Frequency_Avg: {1}", data.RowIndex, data.Frequency.AvgValue);
            if (data.Frequency.MinValue  <= 0)
                return string.Format("Nevalidan podatak Row:{0} Frequency_Min: {1}", data.RowIndex, data.Frequency.MinValue);
            if (data.Frequency.MaxValue <= 0)
                return string.Format("Nevalidan podatak Row:{0} Frequency_Max: {1}", data.RowIndex, data.Frequency.MaxValue );
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
