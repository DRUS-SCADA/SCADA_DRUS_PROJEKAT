using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class RealTimeDriver:IRealTimeDriver
    {
        public static RealTimeUnit RTU = new RealTimeUnit();
        public void SendMessage(double number, string address)
        {
            Console.WriteLine($"Vrednost {number} na adresi {address} \n");
            RTU.SetValue(address, number);
        }
    }
}
