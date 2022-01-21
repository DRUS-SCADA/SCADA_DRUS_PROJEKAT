using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core_WCFService
{
    public class DigitalInput
    {
        public string TagName { get; set; }
        public string Description { get; set; }
        public string IOAdress { get; set; }
        public double ScanTime { get; set; }
        public bool ONOFF_scan {get; set;}

        public DigitalInput() { }

        public DigitalInput (string name, string description, string address, double scanTime, bool onoff)
        {
            this.TagName = name;
            this.Description = description;
            this.IOAdress = address;
            this.ScanTime = scanTime;
            this.ONOFF_scan = onoff;
        }
    }
}
