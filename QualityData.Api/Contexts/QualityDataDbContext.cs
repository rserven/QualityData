using Microsoft.EntityFrameworkCore;
using QualityData.Api.Models;

namespace QualityData.Api.Contexts
{
    public class QualityDataDbContext : DbContext
    {
        public QualityDataDbContext(DbContextOptions<QualityDataDbContext> options)
            : base(options) { }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ClienteUbicacion> ClienteUbicacions { get; set; }
        public virtual DbSet<TipoUbicacion> TipoUbicacions { get; set; }
        public virtual DbSet<Ubicacion> Ubicacions { get; set; }
        public virtual DbSet<ObtenerCliente> ObtenerClientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObtenerCliente>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}
