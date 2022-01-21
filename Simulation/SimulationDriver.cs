using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simulation
{
    public class SimulationDriver
    {
        private Dictionary<string, double> addressValues;
        private object locker = new object();

        public SimulationDriver()
        {
            addressValues = new Dictionary<string, double>();

            addressValues.Add("ADDR001", 0);
            addressValues.Add("ADDR002", 0);
            addressValues.Add("ADDR003", 0);
            addressValues.Add("ADDR004", 0);
            addressValues.Add("ADDR005", 0);
            addressValues.Add("ADDR006", 0);
            addressValues.Add("ADDR007", 0);
            addressValues.Add("ADDR008", 0);
            addressValues.Add("ADDR009", 0);
            addressValues.Add("ADDR010", 0);
        }

        public void StartPLCSimulator()
        {
            Thread t1 = new Thread(GeneratingAnalogInputs);
            t1.Start();

            Thread t2 = new Thread(GeneratingDigitalInputs);
            t2.Start();
        }


        private void GeneratingAnalogInputs()
        {
            while (true)
            {
                Thread.Sleep(100);

                lock (locker)
                {
                    addressValues["ADDR001"] = 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI); //SINE
                    addressValues["ADDR002"] = 100 * DateTime.Now.Second / 60; //RAMP
                    addressValues["ADDR003"] = 100 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
                    addressValues["ADDR004"] = 100 * Math.Exp((double)DateTime.Now.Second / 100);
                }

                //... 
            }
        }


        private void GeneratingDigitalInputs()
        {
            while (true)
            {
                Thread.Sleep(5000);

                lock (locker)
                {
                    if (addressValues["ADDR009"] == 0)
                    {
                        addressValues["ADDR009"] = 1;
                    }
                    else
                    {
                        addressValues["ADDR009"] = 0;
                    }
                }

                //... 
            }
        }

        public double GetValue(string address)
        {
            return addressValues[address];
        }

        public void SetValue(string address, double value)
        {
            addressValues[address] = value;
        }
    }
}
