using Api_GestionVentas.Data;
using Api_GestionVentas.DTOs.SubCategoria;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Api_GestionVentas.Services
{
    public class SubCategoriaService : ISubCategoriaService

    {

        private readonly AppDbContext _context;


        public SubCategoriaService(AppDbContext context)
        {

            _context = context;

        }


        public async Task<IEnumerable<SubCategoria>> GetAllAsync()

        {

            return await _context.Subcategorias.Include(s => s.Categoria).ToListAsync();

        }


        public async Task<SubCategoria> GetByIdAsync(int id)

        {

            return await _context.Subcategorias.Include(s => s.Categoria).FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<SubCategoria> CreateAsync(SubCategoriaCreateDto subcategoria)

        {

            // Validar que la categoría exista y esté activa

            var categoria = await _context.Categorias.FindAsync(subcategoria.CategoriaId);

            if (categoria == null || !categoria.EsActivo)

            {

                throw new InvalidOperationException("La categoría no existe o no está activa.");

            }

            SubCategoria subcategoriaNuevo = new SubCategoria();
            subcategoriaNuevo.Nombre = subcategoria.Nombre;
            subcategoriaNuevo.CategoriaId = subcategoria.CategoriaId;
            subcategoriaNuevo.EsActivo = true;

            

            _context.Subcategorias.Add(subcategoriaNuevo);
            await _context.SaveChangesAsync();
            return subcategoriaNuevo;
        }


        public async Task<SubCategoria> UpdateAsync(int id, SubCategoriaUpdateDto subcategoria)

        {

            var existing = await _context.Subcategorias.FindAsync(id);

            if (existing == null) return null;


            // Validar categoría si se cambia

            if (subcategoria.CategoriaId != existing.CategoriaId)

            {

                var categoria = await _context.Categorias.FindAsync(subcategoria.CategoriaId);
                    
                if (categoria == null || !categoria.EsActivo)

                {

                    throw new InvalidOperationException("La nueva categoría no existe o no está activa.");

                }

            }


            existing.Nombre = subcategoria.Nombre;

            existing.CategoriaId = subcategoria.CategoriaId;

            existing.EsActivo = subcategoria.EsActivo;


            await _context.SaveChangesAsync();

            return existing;

        }


        public async Task<bool> DeactivateAsync(int id)

        {

            var subcategoria = await _context.Subcategorias.FindAsync(id);

            if (subcategoria == null) return false;


            subcategoria.EsActivo = false;

            await _context.SaveChangesAsync();

            return true;

        }

    }
}
