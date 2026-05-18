using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using Common;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IChargingService> factory = new ChannelFactory<IChargingService>("ChargerService");
            IChargingService proxy = factory.CreateChannel();

            ILogger logger = new Logger("log.txt");

            string pathBefore = Menu();
            string[] part = pathBefore.Split('*');
            string path = part[0];
            int number = int.Parse(part[1]);
            
            TextManipulation manipulator = new TextManipulation(path);
            manipulator.Initialize();

            proxy.StartSession(number);

            int brojRedova = manipulator.GetNumberOfLines();
            string dataString = "";
            for (int i = 0; i < brojRedova; i++)
            {
                if(i == 0)
                { 
                    manipulator.ReadLine(); 
                }
                else
                {
                    dataString = manipulator.ReadLine();
                    DataContract data = manipulator.ConvertToData(dataString, number, i);
                    if(data != null)
                    {
                        if(!manipulator.Validate(data))
                            logger.Log("Podaci nemaju ispravne vrednosti za vozilo " + number + " u redu " + i, LogType.WARNING);

                        try
                        {
                            proxy.PushSample(data);
                        }
                        catch (FaultException<CustomException> e)
                        {
                            Console.WriteLine($"ERROR : {e.Detail.Message}");
                        }
                    }
                    else
                    {
                        logger.Log("Podaci su u neispravnom formatu za vozilo " + number + " u redu " + i, LogType.ERROR);
                    }
                }
            }
            manipulator.Dispose();

            proxy.EndSession();
        }

        public static string Menu()
        {
            FolderManipulation folders = new FolderManipulation();
            Console.WriteLine("Dobar dan, molimo vas odaberite jedno od sledecih vozila (1 - 12):  \n");
            int broj = 0;
            while (true)
            {
                folders.PrintAvailableDirectories();
                Console.WriteLine("Unesite broj vozila (1-12): ");
                try
                {
                    broj = int.Parse(Console.ReadLine());
                    if (broj <= 12 && broj >= 1)
                    {
                        break;
                    }
                    Console.WriteLine("\nNeispravan unos, molimo unesite ispravan broj vozila!\n\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nNeispravan unos, molimo unesite broj vozila (1-12)\n\n");
                }
            }
            string odabranFolder = folders.ChooseDirectory(broj);
            odabranFolder += "*" + broj;
            return odabranFolder;
        }
    }
}
