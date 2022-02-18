using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    [DataContract]
    public class Tag
    {
        [DataMember]
        public List<AITag> analogInputs { get; set; }
        [DataMember]
        public List<DITag> digitalInputs { get; set; }

        public Tag()
        {
            analogInputs = new List<AITag>();
            digitalInputs = new List<DITag>();
        }
    }
}
