using Api_GestionVentas.DTOs.Categoria;
using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAllAsync(); 

        Task<Categoria> GetByIdAsync(int id);

        Task<Categoria> CreateAsync(CategoriaCreateDto categoria);

        Task<Categoria> UpdateAsync(int id, CategoriaUpdateDto categoria);

        Task<bool> DeactivateAsync(int id);
    }
}
