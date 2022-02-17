using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(TagProcessing));
            ServiceHost svc1 = new ServiceHost(typeof(ReportManager));

            svc.Open();
            svc1.Open();

            Console.WriteLine("Press enter to exit!");

            Console.ReadLine();

            svc.Close();
            svc1.Close();
        }
    }
}
