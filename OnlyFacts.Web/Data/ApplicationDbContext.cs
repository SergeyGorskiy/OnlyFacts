using Microsoft.EntityFrameworkCore;
using OnlyFacts.Web.Data.Base;

namespace OnlyFacts.Web.Data
{
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Fact> Facts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Notification> Notifications { get; set; }
    }

}
