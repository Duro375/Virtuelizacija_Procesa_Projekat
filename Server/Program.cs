using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---IZBOR MODA POKRETANJA---");
            Console.WriteLine("1. Testiraj prvo IDisposable (FileStream + StreamWriter)");
            Console.WriteLine("2. Pokreni odmah WCF servis");
            Console.Write("Odabir (1 ili 2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("\nPOKRETANJE TESTOVA...\n");
                TestSessionWriterDisposable();
                TestSessionWriterWithInterruption();
                Console.WriteLine("\nPritisni bilo koje dugme da pokrenes WCF servis...");
                RunWCFService();
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("\nPOKRETANJE WCF SERVISA...\n");
                RunWCFService();
            }
        }
        static void TestSessionWriterDisposable()
        {
            Console.WriteLine("\nTEST 1: SessionWriter\n");

            string testFile = "test_session.csv";
            if (File.Exists(testFile)) File.Delete(testFile);

            try
            {
                using (SessionWriter writer = new SessionWriter(testFile))
                {
                    var data = CreateTestData(1, 1);
                    writer.WriteRow(data);
                    Console.WriteLine("Red upisan");
                }
                // automatski poziva Dispose()

                // Provera da li je fajl pravilno zatvoren
                using (FileStream fs = new FileStream(testFile, FileMode.Open, FileAccess.ReadWrite))
                {
                    Console.WriteLine("Fajl je pravilno zatvoren");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fajl je još zaključan! {ex.Message}");
            }
        }
        static void TestSessionWriterWithInterruption()
        {
            Console.WriteLine("TEST 2: SessionWriter(Simulacija prekida)");

            string testFile = "test_interruption.csv";
            if (File.Exists(testFile)) File.Delete(testFile);

            try
            {
                using (SessionWriter writer = new SessionWriter(testFile))
                {
                    // Pisanje prvog reda
                    var data1 = CreateTestData(1, 1);
                    writer.WriteRow(data1);
                    Console.WriteLine("red 1 upisan");

                    // Pisanje drugog reda
                    var data2 = CreateTestData(1, 2);
                    writer.WriteRow(data2);
                    Console.WriteLine("red 2 upisan");

                    // SIMULACIJA PREKIDA NAKON PISANJA
                    Console.WriteLine("\nSIMULACIJA PREKIDA...");
                    throw new System.ServiceModel.CommunicationException(
                        "Konekcija sa klijentom je iznenada prekinuta!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception uhvaćen: {ex.Message}");
            }

            if (File.Exists(testFile))
            {
                string content = File.ReadAllText(testFile);
                Console.WriteLine($"\nREZULTAT: Fajl sadrži {content.Length} bajtova");
                Console.WriteLine($"ZAKLJUČAK: Redovi pisani pre prekida su sačuvani!");
                Console.WriteLine($"\nSadržaj:\n{content}");
            }
        }
        

        static DataContract CreateTestData(int vehicleId, int rowIndex)
        {
            return new DataContract(
                vehicleId: vehicleId,
                rowIndex: rowIndex,
                timeStamp: DateTime.Now,
                voltage_RMS_Min: 207.0,
                voltage_RMS_Max: 253.0,
                voltage_RMS_Avg: 230.5,
                current_RMS_Min: 5.2,
                current_RMS_Max: 95.8,
                current_RMS_Avg: 32.5,
                real_Power_Min: 1200.0,
                real_Power_Max: 22000.0,
                real_Power_Avg: 7500.0,
                reactive_Power_Min: 150.0,
                reactive_Power_Avg: 850.0,
                reactive_Power_Max: 2800.0,
                apparent_Power_Min: 1200.0,
                apparent_Power_Avg: 7650.0,
                apparent_Power_Max: 23000.0,
                frequency_Min: 49.8,
                frequency_Avg: 50.0,
                frequency_Max: 50.2
            );
        }

        static void RunWCFService()
        {
            Publisher publisher = new Publisher();
            Subscriber subscriber = new Subscriber();

            publisher.OnTransferStarted += subscriber.HandleTransferMessage;
            publisher.OnSampleRecieved += subscriber.HandleSampleMessage;
            publisher.OnTransferCompleted += subscriber.HandleTransferMessage; 
            publisher.OnWarningRaised += subscriber.HandleWarningMessage;

            ChargingService service = new ChargingService(publisher);

            using (ServiceHost host = new ServiceHost(service))
            {
                host.Open();
                Console.WriteLine("Servis je otvoren, pritisnite bilo koje dugme da ga zatvorite");
                Console.ReadKey();
                host.Close();
            }
            Console.WriteLine("Servis je zatvoren");
        }
    }
}
