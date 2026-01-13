using Api_GestionVentas.Data;
using Api_GestionVentas.DTOs.Proveedor;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
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


        public async Task<IEnumerable<Proveedor>> GetAllAsync()

        {

            return await _context.Proveedores.ToListAsync();

        }


        public async Task<Proveedor> GetByIdAsync(int id)

        {

            return await _context.Proveedores.FindAsync(id);

        }


        public async Task<Proveedor> CreateAsync(ProveedorCreateDto proveedor)

        {

            Proveedor proveedorNuevo = new Proveedor();
            proveedorNuevo.Nombre = proveedor.Nombre;
            proveedorNuevo.Direccion =  proveedor.Direccion;
            proveedorNuevo.Contacto = proveedor.Contacto;
            proveedorNuevo.EsActivo = true;
            

            _context.Proveedores.Add(proveedorNuevo);

            await _context.SaveChangesAsync();

            return proveedorNuevo;

        }


        public async Task<Proveedor> UpdateAsync(int id, ProveedorUpdateDto proveedor)

        {

            var existing = await _context.Proveedores.FindAsync(id);

            if (existing == null) return null;


            existing.Nombre = proveedor.Nombre;

            existing.Contacto = proveedor.Contacto;

            existing.Direccion = proveedor.Direccion;

            existing.EsActivo = proveedor.EsActivo;


            await _context.SaveChangesAsync();

            return existing;

        }


        public async Task<bool> DeactivateAsync(int id)

        {

            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null) return false;


            proveedor.EsActivo = false;

            await _context.SaveChangesAsync();

            return true;

        }

    }
}
