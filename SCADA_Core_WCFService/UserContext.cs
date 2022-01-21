using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core_WCFService
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
