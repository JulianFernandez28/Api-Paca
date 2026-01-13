using Api_GestionVentas.DTOs.Categoria;
using Api_GestionVentas.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Api_GestionVentas.Services.IServices
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAllAsync(int empresaId); 

        Task<Categoria> GetByIdAsync(int id, int empresaId);

        Task<Categoria> CreateAsync(CategoriaCreateDto categoria, int empresaId);

        Task<Categoria> UpdateAsync(int id, CategoriaUpdateDto categoria, int empresaId);

        Task<bool> DeactivateAsync(int id, int empresaId);
    }
}
