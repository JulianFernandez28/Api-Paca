using Api_GestionVentas.DTOs.Proveedor;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_GestionVentas.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    [Authorize]

    public class ProveedoresController : ControllerBase

    {

        private readonly IProveedorService _proveedorService;


        public ProveedoresController(IProveedorService proveedorService)

        {

            _proveedorService = proveedorService;

        }

        private int GetIdEmpresa()

        {

            var claim = User.FindFirst("EmpresaId");

            if (claim == null) throw new UnauthorizedAccessException("IdEmpresa no encontrado en el token.");

            return int.Parse(claim.Value);

        }

        // GET: api/proveedores

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {
            int empresaId = GetIdEmpresa();
            var proveedores = await _proveedorService.GetAllAsync(empresaId);

            return Ok(proveedores);

        }


        // GET: api/proveedores/5

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {
            int empresaId = GetIdEmpresa();
            var proveedor = await _proveedorService.GetByIdAsync(id,empresaId);

            if (proveedor == null) return NotFound();

            return Ok(proveedor);

        }


        // POST: api/proveedores

        [HttpPost]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Create([FromBody] ProveedorCreateDto proveedor)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            int empresaId = GetIdEmpresa();
            var nuevoProveedor = await _proveedorService.CreateAsync(proveedor, empresaId);

            return CreatedAtAction(nameof(GetById), new { id = nuevoProveedor.Id }, nuevoProveedor);

        }


        // PUT: api/proveedores/5

        [HttpPut("{id}")]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Update(int id, [FromBody] ProveedorUpdateDto proveedor)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            int empresaId = GetIdEmpresa();
            var updated = await _proveedorService.UpdateAsync(id, proveedor,empresaId);

            if (updated == null) return NotFound();

            return Ok(updated);

        }


        // DELETE: api/proveedores/5

        [HttpDelete("{id}")]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Deactivate(int id)

        {
            int empresaId = GetIdEmpresa();
            var result = await _proveedorService.DeactivateAsync(id, empresaId);

            if (!result) return NotFound();

            return NoContent();

        }

    }
}
