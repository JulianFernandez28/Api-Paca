using Api_GestionVentas.DTOs.Categoria;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_GestionVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;


        public CategoriaController(ICategoriaService categoriaService)

        {

            _categoriaService = categoriaService;

        }

        private int GetIdEmpresa()

        {

            var claim = User.FindFirst("EmpresaId");

            if (claim == null) throw new UnauthorizedAccessException("IdEmpresa no encontrado en token.");

            return int.Parse(claim.Value);

        }


        // GET: api/categorias

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {
            var idEmpresa = GetIdEmpresa();
            var categorias = await _categoriaService.GetAllAsync(idEmpresa);

            return Ok(categorias);

        }


        // GET: api/categorias/5

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {
            var idEmpresa = GetIdEmpresa();
            var categoria = await _categoriaService.GetByIdAsync(id,idEmpresa);

            if (categoria == null) return NotFound();

            return Ok(categoria);

        }


        // POST: api/categorias

        [HttpPost]

        [Authorize(Roles = "admin")] // Solo admins pueden crear

        public async Task<IActionResult> Create([FromBody] CategoriaCreateDto categoria)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var idEmpresa = GetIdEmpresa();
            var nuevaCategoria = await _categoriaService.CreateAsync(categoria,idEmpresa);

            return CreatedAtAction(nameof(GetById), new { id = nuevaCategoria.Id }, nuevaCategoria);

        }


        // PUT: api/categorias/5

        [HttpPut("{id}")]

        [Authorize(Roles = "admin")] // Solo admins pueden actualizar

        public async Task<IActionResult> Update(int id, [FromBody] CategoriaUpdateDto categoria)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var idEmpresa = GetIdEmpresa();
            var updated = await _categoriaService.UpdateAsync(id, categoria,idEmpresa);

            if (updated == null) return NotFound();

            return Ok(updated);

        }


        // DELETE: api/categorias/5 (Desactiva en lugar de eliminar)

        [HttpDelete("{id}")]

        [Authorize(Roles = "admin")] // Solo admins pueden desactivar

        public async Task<IActionResult> Deactivate(int id)

        {
            var idEmpresa = GetIdEmpresa();
            var result = await _categoriaService.DeactivateAsync(id,idEmpresa);

            if (!result) return NotFound();

            return NoContent(); // 204: Eliminación lógica exitosa

        }
    }
}
