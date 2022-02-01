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
    public class DigitalOutput : INotifyPropertyChanged
    {
        public DigitalOutput() { }
        public DigitalOutput(string name, string description, string address, double initialValue)
        {
            TagName = name;
            Description = description;
            IOAdress = address;
            InitialValue = initialValue;
        }

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

        public event PropertyChangedEventHandler PropertyChanged;


        /*public string tagName
        {
            get { return TagName; }
            set
            {
                TagName = value;
                OnPropertyChanged("TagName");
            }
        }
        public string description
        {
            get { return Description; }
            set
            {
                Description = value;
                OnPropertyChanged("Description");
            }
        }

        public string address
        {
            get { return IOAdress; }
            set
            {
                IOAdress = value;
                OnPropertyChanged("Address");
            }
        }

        public double initialValue
        {
            get { return InitialValue; }
            set
            {
                InitialValue = value;
                OnPropertyChanged("InitialValue");
            }
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }*/
    }
}
