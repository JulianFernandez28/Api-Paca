using Api_GestionVentas.Data;
using Api_GestionVentas.DTOs.Producto;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Api_GestionVentas.Services
{
    public class ProductoService : IProductoService

    {

        private readonly AppDbContext _context;


        public ProductoService(AppDbContext context)

        {

            _context = context;

        }


        public async Task<IEnumerable<Producto>> GetAllAsync()

        {

            return await _context.Productos.Include(p => p.Proveedor).Include(p => p.Subcategoria).ToListAsync();

        }


        public async Task<Producto> GetByIdAsync(int id)

        {

            return await _context.Productos.Include(p => p.Proveedor).Include(p => p.Subcategoria).FirstOrDefaultAsync(p => p.Id == id);

        }


        public async Task<Producto> CreateAsync(ProductoCreateDto producto)

        {

            var existing = await _context.Productos.FirstOrDefaultAsync(q => q.Codigo == producto.Codigo);
            if (existing!=null)

            {

                throw new InvalidOperationException("El codigó se encuentra asignado a otro producto");

            }

            // Validar que la subcategoría exista y esté activa

            var subcategoria = await _context.Subcategorias.FindAsync(producto.IdSubcategoria);

            if (subcategoria == null || !subcategoria.EsActivo)

            {

                throw new InvalidOperationException("La subcategoría no existe o no está activa.");

            }


            // Validar que el proveedor exista y esté activo

            var proveedor = await _context.Proveedores.FindAsync(producto.IdProveedor);

            if (proveedor == null || !proveedor.EsActivo)

            {

                throw new InvalidOperationException("El proveedor no existe o no está activo.");

            }


            Producto productoNuevo = new Producto();
            productoNuevo.Codigo = producto.Codigo;
            productoNuevo.Nombre = producto.Nombre;
            productoNuevo.Descripcion = producto.Descripcion;
            productoNuevo.Precio = producto.Precio;
            productoNuevo.Stock = producto.Stock;
            productoNuevo.IdProveedor = producto.IdProveedor;
            productoNuevo.IdSubcategoria = producto.IdSubcategoria;
            productoNuevo.EsActivo = true; // Por defecto activa

            _context.Productos.Add(productoNuevo);

            await _context.SaveChangesAsync();

            return productoNuevo;

        }


        public async Task<Producto> UpdateAsync(int id, ProductoUpdateDto producto)

        {

            var existing = await _context.Productos.FindAsync(id);

            if (existing == null) return null;


            // Validar subcategoría si se cambia

            if (producto.IdSubcategoria != existing.IdSubcategoria)

            {

                var subcategoria = await _context.Subcategorias.FindAsync(producto.IdSubcategoria);

                if (subcategoria == null || !subcategoria.EsActivo)

                {

                    throw new InvalidOperationException("La subcategoría no existe o no está activa.");

                }

            }


            // Validar proveedor si se cambia

            if (producto.IdProveedor != existing.IdProveedor)

            {

                var proveedor = await _context.Proveedores.FindAsync(producto.IdProveedor);

                if (proveedor == null || !proveedor.EsActivo)

                {

                    throw new InvalidOperationException("El  proveedor no existe o no está activo.");

                }

            }


            existing.Nombre = producto.Nombre;

            existing.Descripcion = producto.Descripcion;

            existing.Precio = producto.Precio;

            existing.Stock = producto.Stock;

            existing.IdProveedor = producto.IdProveedor;

            existing.IdSubcategoria = producto.IdSubcategoria;

            existing.EsActivo = producto.EsActivo;


            await _context.SaveChangesAsync();

            return existing;

        }


        public async Task<bool> DeactivateAsync(int id)

        {

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null) return false;


            producto.EsActivo = false;

            await _context.SaveChangesAsync();

            return true;

        }

    }
}
