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
            ChannelFactory<IServiceContract> factory = new ChannelFactory<IServiceContract>("ChargerService");
            IServiceContract proxy = factory.CreateChannel();

            string pathBefore = Menu();
            string[] part = pathBefore.Split('*');
            string path = part[0];
            int broj = int.Parse(part[1]);
            
            TextManipulation man = new TextManipulation(path);
            man.Initialize();

            proxy.StartSession();

            int brojRedova = man.GetNumberOfLines();
            string s = "";
            for (int i = 0; i < brojRedova; i++)
            {
                if(i == 0)
                { 
                    man.ReadLine(); 
                }
                else
                {
                    s = man.ReadLine();
                    DataContract data = man.ConvertToData(s, i, broj);
                    if(data != null)
                    {
                        man.Validate(data);
                        proxy.PushSample(data);
                    }
                }
            }
            man.Dispose();

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
