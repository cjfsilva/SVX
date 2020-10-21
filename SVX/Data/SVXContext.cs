using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SVX.Models;

namespace SVX.Data
{
    public class SVXContext : DbContext
    {
        public SVXContext (DbContextOptions<SVXContext> options)
            : base(options)
        {
        }

        public DbSet<SVX.Models.Client> Client { get; set; }

        public DbSet<SVX.Models.Operator> Operator { get; set; }
    }
}
