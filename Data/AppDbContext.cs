using Api_GestionVentas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Api_GestionVentas.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<SubCategoria> Subcategorias { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<DetalleVenta> DetalleVenta { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            // Configuraciones adicionales si es necesario (ej. índices)

            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            // Relación Venta - Detalles

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
