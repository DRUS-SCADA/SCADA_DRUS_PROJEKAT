using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class AITag
    {
        [Key]
        public int Id { get; set; }
        public string TagName { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
    }
}
