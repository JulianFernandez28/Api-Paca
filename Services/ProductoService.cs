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


        public async Task<IEnumerable<Producto>> GetAllAsync(int empresaId)

        {

            return await _context.Productos
                .Where(p => p.EmpresaId == empresaId)
                .Include(p => p.Proveedor)
                .Include(p => p.Subcategoria)
                .ToListAsync();

        }


        public async Task<Producto> GetByIdAsync(int id, int empresaId)

        {

            return await _context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.Subcategoria)
                .FirstOrDefaultAsync(p => p.Id == id && p.EmpresaId == empresaId);

        }


        public async Task<Producto> CreateAsync(ProductoCreateDto producto, int empresaId)

        {

            var existing = await _context.Productos
                .FirstOrDefaultAsync(q => q.Codigo == producto.Codigo && q.EmpresaId == empresaId);
            if (existing!=null)

            {

                throw new InvalidOperationException("El codigó se encuentra asignado a otro producto");

            }

            // Validar que la subcategoría exista y esté activa

            var subcategoria = await _context.Subcategorias
                .FirstOrDefaultAsync(p => p.Id == producto.IdSubcategoria && p.EmpresaId ==  empresaId);

            if (subcategoria == null || !subcategoria.EsActivo)

            {

                throw new InvalidOperationException("La subcategoría no existe o no está activa.");

            }


            // Validar que el proveedor exista y esté activo

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(p => p.Id == producto.IdProveedor && p.EmpresaId == empresaId);

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
            productoNuevo.EsActivo = true;
            productoNuevo.EmpresaId = empresaId;

            _context.Productos.Add(productoNuevo);

            await _context.SaveChangesAsync();

            return productoNuevo;

        }


        public async Task<Producto> UpdateAsync(int id, ProductoUpdateDto producto, int empresaId)

        {

            var existing = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == id && p.EmpresaId == empresaId);

            if (existing == null) return null;


            // Validar subcategoría si se cambia

            if (producto.IdSubcategoria != existing.IdSubcategoria)

            {

                var subcategoria = await _context.Subcategorias
                    .FirstOrDefaultAsync(p => p.Id == producto.IdSubcategoria && p.EmpresaId == empresaId);

                if (subcategoria == null || !subcategoria.EsActivo)

                {

                    throw new InvalidOperationException("La subcategoría no existe o no está activa.");

                }

            }


            // Validar proveedor si se cambia

            if (producto.IdProveedor != existing.IdProveedor)

            {

                var proveedor = await _context.Proveedores
                    .FirstOrDefaultAsync(p => p.Id == producto.IdProveedor && p.EmpresaId == empresaId);

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

            existing.EmpresaId = empresaId;

            await _context.SaveChangesAsync();

            return existing;

        }


        public async Task<bool> DeactivateAsync(int id, int empresaId)

        {

            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == id && p.EmpresaId == empresaId);

            if (producto == null) return false;


            producto.EsActivo = false;

            await _context.SaveChangesAsync();

            return true;

        }

    }
}
