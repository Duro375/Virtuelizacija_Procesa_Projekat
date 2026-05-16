using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Common;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = Menu();
            Console.WriteLine(path);
        }

        public static string Menu()
        {
            FolderManipulation folders = new FolderManipulation();
            Console.WriteLine("Dobar dan, molimo vas odaberite jedno od sledecih vozila (1 - 12):  \n");
            int broj = 0;
            while(true)
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
            return odabranFolder;
        }
    }
}
