using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class AnalogInput
    {
        public string TagName { get; set; }
        public string Description { get; set; }
        public string IOAdress { get; set; }
        public double ScanTime { get; set; }
        public bool ONOFF_scan { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }

        public AnalogInput() { }

        public AnalogInput(string name, string description, string address, double scanTime, bool onoff, double low, double high, string units)
        {
            this.TagName = name;
            this.Description = description;
            this.IOAdress = address;
            this.ScanTime = scanTime;
            this.ONOFF_scan = onoff;
            this.LowLimit = low;
            this.HighLimit = high;
            this.Units = units;
        }
    }
}
