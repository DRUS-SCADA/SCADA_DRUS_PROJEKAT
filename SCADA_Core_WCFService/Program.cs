using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core_WCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(TagProcessing));

            svc.Open();

            Console.WriteLine("Press enter to exit!");

            Console.ReadLine();

            svc.Close();
        }
    }
}
