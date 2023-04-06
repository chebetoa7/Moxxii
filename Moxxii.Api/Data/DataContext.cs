using Microsoft.EntityFrameworkCore;
using Moxxii.Shared.Entities;

namespace Moxxii.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options  ) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Password).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(x => x.PhoneNumber).IsUnique();

        }
    }
}
