using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface IVentaService
    {
        Task<IEnumerable<Venta>> GetAllAsync(int empresaId);

        Task<Venta> GetByIdAsync(int id,int empresaId);

        Task<Venta> CreateAsync(Venta venta, int idUsuario, int empresaId);
    }
}
