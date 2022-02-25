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
    public class DigitalOutput
    {
        public DigitalOutput() { }

        [DataMember]
        [Key]
        public int Id { get; set; }
        [DataMember]
        public string tag_name { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string IO_Adress { get; set; }
        [DataMember]
        public double initial_Value { get; set; }

    }
}
