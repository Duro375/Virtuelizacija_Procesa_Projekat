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

            session.FirstRow();
            rejects.FirstRow();

            Console.WriteLine("Prenos je u toku...");
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
                    }
                }

            }

        }
        public void EndSession()
        {
            Console.WriteLine("Prenos je zavrsen");
            session.Dispose();
            rejects.Dispose();
        }

        //3. zadatak: Validacija i fault poruka
        private string Validate(DataContract data)
        {
            if(data.TimeStamp <= DateTime.MinValue)
                return "Invalid Timestapm";
            if(data.Voltage_RMS_Avg <= 0)
                return string.Format("Nevalidan podatak Row:{0} Voltage_RMS_Avg: {1}", data.RowIndex, data.Voltage_RMS_Avg);
            if (data.Voltage_RMS_Min <= 0)
                return string.Format("Nevalidan podatak Row:{0} Voltage_RMS_Min: {1}", data.RowIndex, data.Voltage_RMS_Min);
            if (data.Voltage_RMS_Max <= 0)
                return string.Format("Nevalidan podatak Row:{0} Voltage_RMS_Max: {1}", data.RowIndex, data.Voltage_RMS_Max);

            if (data.Current_RMS_Avg <= 0)
                return string.Format("Nevalidan podatak Row:{0} Current_RMS_Avg: {1}",data.RowIndex, data.Current_RMS_Avg);
            if (data.Current_RMS_Min <= 0)
                return string.Format("Nevalidan podatak Row:{0} Current_RMS_Min: {1}", data.RowIndex, data.Current_RMS_Min);
            if (data.Current_RMS_Max <= 0)
                return string.Format("Nevalidan podatak Row:{0} Current_RMS_Max: {1}", data.RowIndex, data.Current_RMS_Max);

            if (data.Real_Power_Avg <= 0)
                return string.Format("Nevalidan podatak Row:{0} Real_Power_Avg: {1}", data.RowIndex, data.Real_Power_Avg);
            if (data.Real_Power_Min <= 0)
                return string.Format("Nevalidan podatak Row:{0} Real_Power_Min: {1}", data.RowIndex, data.Real_Power_Min);
            if (data.Real_Power_Max <= 0)
                return string.Format("Nevalidan podatak Row:{0} Real_Power_Max: {1}", data.RowIndex, data.Real_Power_Max);

            if (data.Reactive_Power_Avg >= 0)
                return string.Format("Nevalidan podatak Row:{0} Reactive_Power_Avg: {1}", data.RowIndex, data.Reactive_Power_Avg);
            if (data.Reactive_Power_Min >= 0)
                return string.Format("Nevalidan podatak Row:{0} Reactive_Power_Min: {1}", data.RowIndex, data.Reactive_Power_Min);
            if (data.Reactive_Power_Max >= 0)
                return string.Format("Nevalidan podatak Row:{0} Reactive_Power_Max: {1}", data.RowIndex, data.Reactive_Power_Max);

            if (data.Apparent_Power_Avg <= 0)
                return string.Format("Nevalidan podatak Row:{0} Apparent_Power_Avg: {1}", data.RowIndex, data.Apparent_Power_Avg);
            if (data.Apparent_Power_Min <= 0)
                return string.Format("Nevalidan podatak Row:{0} Apparent_Power_Min: {1}", data.RowIndex, data.Apparent_Power_Min);
            if (data.Apparent_Power_Max <= 0)
                return string.Format("Nevalidan podatak Row:{0} Apparent_Power_Max: {1}", data.RowIndex, data.Apparent_Power_Max);


            if (data.Frequency_Avg <= 0)
                return string.Format("Nevalidan podatak Row:{0} Frequency_Avg: {1}", data.RowIndex, data.Frequency_Avg);
            if (data.Frequency_Min <= 0)
                return string.Format("Nevalidan podatak Row:{0} Frequency_Min: {1}", data.RowIndex, data.Frequency_Min);
            if (data.Frequency_Max <= 0)
                return string.Format("Nevalidan podatak Row:{0} Frequency_Max: {1}", data.RowIndex, data.Frequency_Max);
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
