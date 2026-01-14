using Api_GestionVentas.DTOs.Producto;
using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync(int empresaId);

        Task<Producto> GetByIdAsync(int id, int empresaId);

        Task<Producto> CreateAsync(ProductoCreateDto producto, int empresaId);

        Task<Producto> UpdateAsync(int id, ProductoUpdateDto producto, int empresaId);

        Task<bool> DeactivateAsync(int id, int empresaId);
    }
}
