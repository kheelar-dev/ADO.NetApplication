using ADO.NetApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetApplication
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(): base("name=DefaultConnection")
        {

        }

        public DbSet<Staff> Staffs { get; set; }
    }
}
