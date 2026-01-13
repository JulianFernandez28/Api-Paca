using Api_GestionVentas.DTOs.Producto;
using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync();

        Task<Producto> GetByIdAsync(int id);

        Task<Producto> CreateAsync(ProductoCreateDto producto);

        Task<Producto> UpdateAsync(int id, ProductoUpdateDto producto);

        Task<bool> DeactivateAsync(int id);
    }
}
