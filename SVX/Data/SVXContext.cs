using Microsoft.EntityFrameworkCore;
using SVX.Models;

namespace SVX.Data {
    public class SVXContext : DbContext
    {
        public SVXContext (DbContextOptions<SVXContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Operator> Operator { get; set; }
        public DbSet<Server> Server { get; set; }
    }
}
