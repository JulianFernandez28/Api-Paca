using Api_GestionVentas.Data;
using Api_GestionVentas.DTOs.Proveedor;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Api_GestionVentas.Services
{
    public class ProveedorService : IProveedorService

    {

        private readonly AppDbContext _context;


        public ProveedorService(AppDbContext context)

        {

            _context = context;

        }


        public async Task<IEnumerable<Proveedor>> GetAllAsync(int empresaId)

        {

            return await _context.Proveedores
                .Where(p => p.EmpresaId ==empresaId)
                .ToListAsync();

        }


        public async Task<Proveedor> GetByIdAsync(int id, int empresaId)

        {

            return await _context.Proveedores
                .FirstOrDefaultAsync(p => p.Id == id && p.EmpresaId == empresaId);

        }


        public async Task<Proveedor> CreateAsync(ProveedorCreateDto proveedor, int empresaId)

        {
            Proveedor existing = await _context.Proveedores
                .FirstOrDefaultAsync( p => p.Nombre == proveedor.Nombre && p.EmpresaId == empresaId);

            if(existing != null)
            {
                throw new InvalidOperationException("El proveedor se encuentra registrado");
            }


            Proveedor proveedorNuevo = new Proveedor();
            proveedorNuevo.Nombre = proveedor.Nombre;
            proveedorNuevo.Direccion =  proveedor.Direccion;
            proveedorNuevo.Contacto = proveedor.Contacto;
            proveedorNuevo.EsActivo = true;
            proveedorNuevo.EmpresaId = empresaId;
            

            _context.Proveedores.Add(proveedorNuevo);

            await _context.SaveChangesAsync();

            return proveedorNuevo;

        }


        public async Task<Proveedor> UpdateAsync(int id, ProveedorUpdateDto proveedor,int empresaId)

        {

            var existing = await _context.Proveedores.FirstOrDefaultAsync( p => p.Id == id && p.EmpresaId ==  empresaId);

            if (existing == null) return null;


            existing.Nombre = proveedor.Nombre;

            existing.Contacto = proveedor.Contacto;

            existing.Direccion = proveedor.Direccion;

            existing.EsActivo = proveedor.EsActivo;

            existing.EmpresaId = empresaId;


            await _context.SaveChangesAsync();

            return existing;

        }


        public async Task<bool> DeactivateAsync(int id,int empresaId)

        {

            var proveedor = await _context.Proveedores.FirstOrDefaultAsync( p => p.Id == id && p.EmpresaId == empresaId);

            if (proveedor == null) return false;


            proveedor.EsActivo = false;

            await _context.SaveChangesAsync();

            return true;

        }

    }
}
