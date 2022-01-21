using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core_WCFService
{
    public class DigitalOutput
    {
        public string TagName { get; set; }
        public string Description { get; set; }
        public string IOAdress { get; set; }
        public bool InitialValue{ get; set; }

        public DigitalOutput() { }

        public DigitalOutput(string name, string description, string address, bool initialValue)
        {
            this.TagName = name;
            this.Description = description;
            this.IOAdress = address;
            this.InitialValue = initialValue;
        }
    }
}
