using Api_GestionVentas.Data;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Api_GestionVentas.Services
{
    public class VentaService:IVentaService
    {

        private readonly AppDbContext _context;


        public VentaService(AppDbContext context)

        {

            _context = context;

        }


        public async Task<IEnumerable<Venta>> GetAllAsync(int empresaId)

        {

            return await _context.Ventas
                .Where(v => v.EmpresaId == empresaId)
                .Include(v => v.DetalleVenta)
                .ThenInclude(d => d.Producto)
                .ToListAsync();

        }


        public async Task<Venta> GetByIdAsync(int id, int empresaId)

        {

            return await _context.Ventas.
                Include(v => v.DetalleVenta)
                .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(v => v.Id == id && v.EmpresaId == empresaId);

        }


        public async Task<Venta> CreateAsync(Venta venta, int usuarioId, int empresaId)

        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Asignar usuario y fecha
                venta.UsuarioId = usuarioId;
                venta.Fecha = DateTime.Now;
                venta.EmpresaId = empresaId;
                decimal total = 0;

                foreach (var detalle in venta.DetalleVenta)

                {
                    // Validar producto
                    var producto = await _context.Productos.FindAsync(detalle.ProductoId);
                    if (producto == null || !producto.EsActivo)
                    {
                        throw new InvalidOperationException($"Producto {detalle.ProductoId} no existe o no está activo.");
                    }
                    // Validar stock
                    if (producto.Stock < detalle.Cantidad)
                    {
                        throw new InvalidOperationException($"Stock insuficiente para {producto.Nombre}. Disponible: {producto.Stock}.");
                    }
                    // Calcular precio unitario y subtotal

                    if (producto.Precio != 0)
                    {
                        detalle.PrecioUnitario = producto.Precio;
                    }
                    
                    total += detalle.PrecioUnitario * detalle.Cantidad;
                    // Reducir stock
                    producto.Stock -= detalle.Cantidad;
                }
                venta.Total = total;
                // Guardar venta y detalles
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return venta;
            }

            catch

            {

                await transaction.RollbackAsync();

                throw;

            }

        }
    }
}
