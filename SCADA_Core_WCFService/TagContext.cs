using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class TagContext : DbContext
    {
        public DbSet<AITag> AITags { get; set; }
        public DbSet<DITag> DITags { get; set; }
    }
}
