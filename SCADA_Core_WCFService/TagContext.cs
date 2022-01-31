using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class TagContext:DbContext
    {
        public DbSet<AnalogOutput> analogOutputs { get; set; }
        public DbSet<DigitalOutput> digitalOutputs { get; set; }
    }
}
