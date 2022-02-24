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
    public class RTUAddress
    {
        [Key]
        public int id { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public bool isFree { get; set; }
    }
}
