using Api_GestionVentas.Data;
using Api_GestionVentas.DTOs.Categoria;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api_GestionVentas.Services
{
    public class CategoriaService: ICategoriaService

    {

        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CategoriaService(AppDbContext context, IHttpContextAccessor httpContextAccessor)

        {

            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IEnumerable<Categoria>> GetAllAsync(int empresaId)

        {

            return await _context.Categorias.Where(e => e.EmpresaId == empresaId).ToListAsync();

        }


        public async Task<Categoria> GetByIdAsync(int id, int empresaId)

        {

            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id && c.EmpresaId == empresaId);

        }


        public async Task<Categoria> CreateAsync(CategoriaCreateDto categoria, int empresaId)

        {

           
            

            Categoria existe = await _context.Categorias.FirstOrDefaultAsync(q => q.Nombre == categoria.Nombre && q.EmpresaId == empresaId);
            if (existe != null)
            {

                throw new InvalidOperationException("La categoria ya se encuentra registrada.");
            }

            Categoria categoriaNueva = new Categoria();
            categoriaNueva.Nombre =  categoria.Nombre;
            categoriaNueva.Descripcion = categoria.Descripcion;
            categoriaNueva.EsActivo = true;
            categoriaNueva.EmpresaId = empresaId;

            _context.Categorias.Add(categoriaNueva);

            await _context.SaveChangesAsync();

            return categoriaNueva;

        }


        public async Task<Categoria> UpdateAsync(int id, CategoriaUpdateDto categoria, int empresaId)

        {

            var existing = await _context.Categorias.FirstOrDefaultAsync(c => c.Id ==  id && c.EmpresaId == empresaId);

            if (existing == null) return null;


            existing.Nombre = categoria.Nombre;

            existing.Descripcion = categoria.Descripcion;

            existing.EsActivo = categoria.EsActivo; // Permite activar/desactivar


            await _context.SaveChangesAsync();

            return existing;

        }


        public async Task<bool> DeactivateAsync(int id, int empresaId)

        {

            var categoria = await _context.Categorias.FirstOrDefaultAsync(_ => _.Id == id && _.EmpresaId == empresaId);

            if (categoria == null) return false;


            categoria.EsActivo = false;

            await _context.SaveChangesAsync();

            return true;

        }

    }
}
