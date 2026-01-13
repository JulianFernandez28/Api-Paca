using Api_GestionVentas.DTOs.Proveedor;
using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface IProveedorService

    {

        Task<IEnumerable<Proveedor>> GetAllAsync();

        Task<Proveedor> GetByIdAsync(int id);

        Task<Proveedor> CreateAsync(ProveedorCreateDto proveedor);

        Task<Proveedor> UpdateAsync(int id, ProveedorUpdateDto proveedor);

        Task<bool> DeactivateAsync(int id);

    }
}
