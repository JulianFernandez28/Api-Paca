using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface IVentaService
    {
        Task<IEnumerable<Venta>> GetAllAsync();

        Task<Venta> GetByIdAsync(int id);

        Task<Venta> CreateAsync(Venta venta, int idUsuario);
    }
}
