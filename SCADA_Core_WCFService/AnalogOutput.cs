using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class AnalogOutput
    {
        public string TagName { get; set; }
        public string Description { get; set; }
        public string IOAdress { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }

        public AnalogOutput() { }

        public AnalogOutput(string name, string description, string address, double scanTime, bool onoff, double initial, double low, double high)
        {
            this.TagName = name;
            this.Description = description;
            this.IOAdress = address;
            this.InitialValue = initial;
            this.HighLimit = high;
            this.LowLimit = low;
        }
    }
}
