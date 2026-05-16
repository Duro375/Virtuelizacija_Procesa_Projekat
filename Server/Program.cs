using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ServiceContract)))
            {
                host.Open();
                Console.WriteLine("Service is open, press any key to close it");
                Console.ReadKey();
                host.Close();
            }
            Console.WriteLine("Service is closed");
            Console.ReadKey();
        }
    }
}
