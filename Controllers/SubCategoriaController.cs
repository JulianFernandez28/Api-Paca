using Api_GestionVentas.DTOs.SubCategoria;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_GestionVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaController : ControllerBase
    {
        private readonly ISubCategoriaService  _subcategoriaService;


        public SubCategoriaController(ISubCategoriaService subcategoriaService)
        {

            _subcategoriaService = subcategoriaService;

        }


        // GET: api/subcategorias

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var subcategorias = await _subcategoriaService.GetAllAsync();

            return Ok(subcategorias);

        }


        // GET: api/subcategorias/5

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {

            var subcategoria = await _subcategoriaService.GetByIdAsync(id);

            if (subcategoria == null) return NotFound();

            return Ok(subcategoria);
        }


        // POST: api/subcategorias

        [HttpPost]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Create([FromBody] SubCategoriaCreateDto subcategoria)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            try

            {

                var nuevaSubcategoria = await _subcategoriaService.CreateAsync(subcategoria);

                return CreatedAtAction(nameof(GetById), new { id = nuevaSubcategoria.Id }, nuevaSubcategoria);

            }

            catch (InvalidOperationException ex)

            {

                return BadRequest(new { Message = ex.Message });

            }
        }


        // PUT: api/subcategorias/5

        [HttpPut("{id}")]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Update(int id, [FromBody] SubCategoriaUpdateDto subcategoria)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            try

            {

                var updated = await _subcategoriaService.UpdateAsync(id, subcategoria);

                if (updated == null) return NotFound();

                return Ok(updated);

            }

            catch (InvalidOperationException ex)

            {

                return BadRequest(new { Message = ex.Message });

            }

        }


        // DELETE: api/subcategorias/5

        [HttpDelete("{id}")]

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Deactivate(int id)

        {

            var result = await _subcategoriaService.DeactivateAsync(id);

            if (!result) return NotFound();

            return NoContent();

        }
    }
}
