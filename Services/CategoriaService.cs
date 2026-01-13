using Api_GestionVentas.Data;
using Api_GestionVentas.DTOs.Categoria;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api_GestionVentas.Services
{
    public class CategoriaService: ICategoriaService

    {

        private readonly AppDbContext _context;


        public CategoriaService(AppDbContext context)

        {

            _context = context;

        }


        public async Task<IEnumerable<Categoria>> GetAllAsync()

        {

            return await _context.Categorias.ToListAsync();

        }


        public async Task<Categoria> GetByIdAsync(int id)

        {

            return await _context.Categorias.FindAsync(id);

        }


        public async Task<Categoria> CreateAsync(CategoriaCreateDto categoria)

        {
            Categoria existe = await _context.Categorias.FirstOrDefaultAsync(q => q.Nombre == categoria.Nombre);
            if (existe != null)
            {

                throw new InvalidOperationException("La categoria ya se encuentra registrada.");
            }

            Categoria categoriaNueva = new Categoria();
            categoriaNueva.Nombre =  categoria.Nombre;
            categoriaNueva.Descripcion = categoria.Descripcion;
            categoriaNueva.EsActivo = true;

            _context.Categorias.Add(categoriaNueva);

            await _context.SaveChangesAsync();

            return categoriaNueva;

        }


        public async Task<Categoria> UpdateAsync(int id, CategoriaUpdateDto categoria)

        {

            var existing = await _context.Categorias.FindAsync(id);

            if (existing == null) return null;


            existing.Nombre = categoria.Nombre;

            existing.Descripcion = categoria.Descripcion;

            existing.EsActivo = categoria.EsActivo; // Permite activar/desactivar


            await _context.SaveChangesAsync();

            return existing;

        }


        public async Task<bool> DeactivateAsync(int id)

        {

            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null) return false;


            categoria.EsActivo = false;

            await _context.SaveChangesAsync();

            return true;

        }

    }
}
