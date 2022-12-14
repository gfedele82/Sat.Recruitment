using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Common;

namespace Sat.Recruitment.DataAccess
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Schema.User>().ToTable(DBTables.DBUsers);
        }



        public virtual DbSet<Schema.User> Users { get; set; }
    }
}
