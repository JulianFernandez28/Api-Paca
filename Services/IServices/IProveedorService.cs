using Api_GestionVentas.DTOs.Proveedor;
using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface IProveedorService

    {

        Task<IEnumerable<Proveedor>> GetAllAsync(int empresaId);

        Task<Proveedor> GetByIdAsync(int id, int empresaId);

        Task<Proveedor> CreateAsync(ProveedorCreateDto proveedor, int empresaId);

        Task<Proveedor> UpdateAsync(int id, ProveedorUpdateDto proveedor, int empresaId);

        Task<bool> DeactivateAsync(int id, int empresaId);

    }
}
