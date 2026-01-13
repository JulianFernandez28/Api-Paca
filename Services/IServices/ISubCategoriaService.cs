using Api_GestionVentas.DTOs.SubCategoria;
using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface ISubCategoriaService
    {
        Task<IEnumerable<SubCategoria>> GetAllAsync();

        Task<SubCategoria> GetByIdAsync(int id);
        Task<SubCategoria> CreateAsync(SubCategoriaCreateDto subcategoria);
        Task<SubCategoria> UpdateAsync(int id, SubCategoriaUpdateDto subcategoria);

        Task<bool> DeactivateAsync(int id);
    }
}
