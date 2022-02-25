using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
