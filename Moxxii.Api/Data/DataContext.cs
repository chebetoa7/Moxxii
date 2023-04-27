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
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Rutas_Paradas> Paradas { get; set; }
        public DbSet<Promociones> Promociones { get; set; }
        public DbSet<SolicitudViaje> solicitudViajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Password).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(x => x.PhoneNumber).IsUnique();

            modelBuilder.Entity<Viaje>().HasIndex(x => x.Id).IsUnique();

            modelBuilder.Entity<Rutas_Paradas>().HasIndex(x => x.Id).IsUnique();

            modelBuilder.Entity<Promociones>().HasIndex(x => x.Id).IsUnique();

            modelBuilder.Entity<SolicitudViaje>().HasIndex(x => x.Id).IsUnique();

        }
    }
}
