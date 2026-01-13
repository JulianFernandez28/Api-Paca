using Api_GestionVentas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Http;
namespace Api_GestionVentas.Data
{
    public class AppDbContext:DbContext
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)

        {

            _httpContextAccessor = httpContextAccessor;

        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<SubCategoria> Subcategorias { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<DetalleVenta> DetalleVenta { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {


            modelBuilder.Entity<Venta>()

                .HasMany(v => v.DetalleVenta)

                .WithOne(d => d.Venta)

                .HasForeignKey(d => d.VentaId);


            // Relación DetalleVenta - Producto

            modelBuilder.Entity<DetalleVenta>()

                .HasOne(d => d.Producto)

                .WithMany(p => p.DetallesVenta)

                .HasForeignKey(d => d.ProductoId);

        }
    }
}
