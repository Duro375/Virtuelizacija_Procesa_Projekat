using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] paths = path.Split(Path.DirectorySeparatorChar);
            string newPath = "";
            for (int i = 0; i < paths.Length - 4; i++)
            {
                newPath += paths[i] + Path.DirectorySeparatorChar;
            }
            newPath += "Datasets" + Path.DirectorySeparatorChar + "BMW iX xDrive50" + Path.DirectorySeparatorChar + "Charging_Profile.csv";
            Console.WriteLine(newPath);
            TextReader textReader = new StreamReader(newPath);
            string abc = textReader.ReadLine();
            abc = textReader.ReadLine();
            Console.WriteLine(abc);
        }
    }
}
