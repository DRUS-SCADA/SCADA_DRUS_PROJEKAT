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
        /*public DigitalOutput(string name, string description, string address, double initialValue)
        {
            TagName = name;
            Description = description;
            IOAdress = address;
            InitialValue = initialValue;
        }*/

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

        //public event PropertyChangedEventHandler PropertyChanged;

        /*public string TagName
        {
            get { return tag_name; }
            set
            {
                tag_name = value;
                OnPropertyChanged("TagName");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Address
        {
            get { return IO_Adress; }
            set
            {
                IO_Adress = value;
                OnPropertyChanged("Address");
            }
        }
        public double InitialValue
        {
            get { return initial_Value; }
            set
            {
                initial_Value = value;
                OnPropertyChanged("InitialValue");
            }
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }*/
    }
}
