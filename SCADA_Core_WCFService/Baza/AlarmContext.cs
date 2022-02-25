using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class AlarmContext:DbContext
    {
        public DbSet<Alarm> Alarms { get; set; }
    }
}
