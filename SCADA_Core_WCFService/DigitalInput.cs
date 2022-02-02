using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    [DataContract]
    public class DigitalInput
    {
        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string IOAdress { get; set; }
        [DataMember]
        public double ScanTime { get; set; }
        [DataMember]
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
