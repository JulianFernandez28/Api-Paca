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


        // GET: api/proveedores

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var proveedores = await _proveedorService.GetAllAsync();

            return Ok(proveedores);

        }


        // GET: api/proveedores/5

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {

            var proveedor = await _proveedorService.GetByIdAsync(id);

            if (proveedor == null) return NotFound();

            return Ok(proveedor);

        }


        // POST: api/proveedores

        [HttpPost]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Create([FromBody] ProveedorCreateDto proveedor)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var nuevoProveedor = await _proveedorService.CreateAsync(proveedor);

            return CreatedAtAction(nameof(GetById), new { id = nuevoProveedor.Id }, nuevoProveedor);

        }


        // PUT: api/proveedores/5

        [HttpPut("{id}")]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Update(int id, [FromBody] ProveedorUpdateDto proveedor)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var updated = await _proveedorService.UpdateAsync(id, proveedor);

            if (updated == null) return NotFound();

            return Ok(updated);

        }


        // DELETE: api/proveedores/5

        [HttpDelete("{id}")]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Deactivate(int id)

        {

            var result = await _proveedorService.DeactivateAsync(id);

            if (!result) return NotFound();

            return NoContent();

        }

    }
}
