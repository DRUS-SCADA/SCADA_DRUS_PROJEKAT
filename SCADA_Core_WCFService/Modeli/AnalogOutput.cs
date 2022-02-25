using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    [DataContract]
    public class AnalogOutput
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
        public double InitialValue { get; set; }
        [DataMember]
        public double LowLimit { get; set; }
        [DataMember]
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
