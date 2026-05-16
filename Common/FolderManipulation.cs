using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FolderManipulation
    {
        public string[] GetListOfAllDirectories()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] paths = path.Split(Path.DirectorySeparatorChar);
            string newPath = "";
            for (int i = 0; i < paths.Length - 4; i++)
            {
                newPath += paths[i] + Path.DirectorySeparatorChar;
            }
            newPath += "Datasets";
            try
            {
                return Directory.GetDirectories(newPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void PrintAvailableDirectories()
        {
            string[] directories = GetListOfAllDirectories();
            if (directories != null)
            {
                int i = 1;
                foreach (string directory in directories)
                {
                    string[] dir = directory.Split(Path.DirectorySeparatorChar);
                    Console.WriteLine(i + ") " + dir[dir.Length - 1]);
                    i++;
                }
            }
        }

        public string ChooseDirectory(int number)
        {
            string[] directories = GetListOfAllDirectories();
            string chosenDirectory = "";
            if (directories != null)
            {
                int i = 1;
                foreach (string directory in directories)
                {
                    if(i == number)
                    {
                        chosenDirectory = directory + Path.DirectorySeparatorChar + "Charging_Profile.csv";
                        break;
                    }
                    i++;
                }
            }
            return chosenDirectory;
        }
    }
}
