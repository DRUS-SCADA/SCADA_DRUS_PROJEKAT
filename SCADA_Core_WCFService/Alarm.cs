using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public enum Types
    {
        LOW,
        HIGH
    }
   public enum Priorities
    {
        value1 = 1,
        value2 = 2,
        value3 = 3
    }
    [DataContract]
    public class Alarm
    {
        [Key]
        public int Id { get; set; }
        [DataMember]
        public Types Types { get; set; }
        [DataMember]
        public Priorities Priorities { get; set; }
        [DataMember]
        public double Treshold { get; set; }
        [DataMember]
        public string TagName { get; set; }
    }
}
