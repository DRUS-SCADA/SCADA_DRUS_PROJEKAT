using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        ONE = 1,
        TWO = 2,
        THREE = 3
    }
    public enum State
    {
        IN,
        OUT
    }
    [DataContract]
    public class Alarm
    {
        [Key]
        public int Id { get; set; }
        [DataMember]
        [NotMapped]
        public Types Types { get; set; }
        [DataMember]
        [NotMapped]
        public Priorities Priorities { get; set; }
        [DataMember]
        public double Treshold { get; set; }
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        [NotMapped]
        public State State { get; set; }
        [DataMember]
        [NotMapped]
        public string Message { get; set; }
        [Column("Types")]
        public string TypeString
        {
            get { return Types.ToString(); }
            private set { Types = value.ParseEnum<Types>(); }
        }
        public Alarm () { }
    }
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
