using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class RealTimeUnit
    {
        private Dictionary<string, double> addressValues;
        public RealTimeUnit()
        {
            addressValues = new Dictionary<string, double>();

            addressValues.Add("ADDR013", 0);
            addressValues.Add("ADDR014", 0);
            addressValues.Add("ADDR015", 0);
            addressValues.Add("ADDR016", 0);
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
