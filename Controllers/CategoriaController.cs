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


        // GET: api/categorias

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var categorias = await _categoriaService.GetAllAsync();

            return Ok(categorias);

        }


        // GET: api/categorias/5

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {

            var categoria = await _categoriaService.GetByIdAsync(id);

            if (categoria == null) return NotFound();

            return Ok(categoria);

        }


        // POST: api/categorias

        [HttpPost]

        [Authorize(Roles = "admin")] // Solo admins pueden crear

        public async Task<IActionResult> Create([FromBody] CategoriaCreateDto categoria)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var nuevaCategoria = await _categoriaService.CreateAsync(categoria);

            return CreatedAtAction(nameof(GetById), new { id = nuevaCategoria.Id }, nuevaCategoria);

        }


        // PUT: api/categorias/5

        [HttpPut("{id}")]

        [Authorize(Roles = "admin")] // Solo admins pueden actualizar

        public async Task<IActionResult> Update(int id, [FromBody] CategoriaUpdateDto categoria)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var updated = await _categoriaService.UpdateAsync(id, categoria);

            if (updated == null) return NotFound();

            return Ok(updated);

        }


        // DELETE: api/categorias/5 (Desactiva en lugar de eliminar)

        [HttpDelete("{id}")]

        [Authorize(Roles = "admin")] // Solo admins pueden desactivar

        public async Task<IActionResult> Deactivate(int id)

        {

            var result = await _categoriaService.DeactivateAsync(id);

            if (!result) return NotFound();

            return NoContent(); // 204: Eliminación lógica exitosa

        }
    }
}
