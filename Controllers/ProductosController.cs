using Api_GestionVentas.Data;
using Api_GestionVentas.DTOs.Producto;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class ProductosController : ControllerBase

{

    private readonly IProductoService _productoService;


    public ProductosController(IProductoService productoService)

    {

        _productoService = productoService;

    }

    private int GetIdEmpresa()

    {

        var claim = User.FindFirst("EmpresaId");

        if (claim == null) throw new UnauthorizedAccessException("IdEmpresa no encontrado en el token.");

        return int.Parse(claim.Value);

    }


    // GET: api/productos

    [HttpGet]

    public async Task<IActionResult> GetAll()

    {
        int empresaId = GetIdEmpresa();
        var productos = await _productoService.GetAllAsync(empresaId);

        return Ok(productos);

    }


    // GET: api/productos/5

    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id)

    {
        int empresaId = GetIdEmpresa();
        var producto = await _productoService.GetByIdAsync(id, empresaId);

        if (producto == null) return NotFound();

        return Ok(producto);

    }


    // POST: api/productos

    [HttpPost]

    [Authorize(Roles = "admin")]

    public async Task<IActionResult> Create([FromBody] ProductoCreateDto producto)

    {

        if (!ModelState.IsValid) return BadRequest(ModelState);


        try

        {

            int empresaId = GetIdEmpresa();
            var nuevoProducto = await _productoService.CreateAsync(producto, empresaId);

            return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.Id }, nuevoProducto);

        }

        catch (InvalidOperationException ex)

        {

            return BadRequest(new { Message = ex.Message });

        }

    }


    // PUT: api/productos/5

    [HttpPut("{id}")]

    [Authorize(Roles = "admin")]

    public async Task<IActionResult> Update(int id, [FromBody] ProductoUpdateDto producto)

    {

        if (!ModelState.IsValid) return BadRequest(ModelState);


        try

        {
            int empresaId = GetIdEmpresa();
            var updated = await _productoService.UpdateAsync(id, producto,empresaId);

            if (updated == null) return NotFound();

            return Ok(updated);

        }

        catch (InvalidOperationException ex)

        {

            return BadRequest(new { Message = ex.Message });

        }

    }


    // DELETE: api/productos/5

    [HttpDelete("{id}")]

    [Authorize(Roles = "admin")]

    public async Task<IActionResult> Deactivate(int id)

    {
        int empresaId = GetIdEmpresa();
        var result = await _productoService.DeactivateAsync(id, empresaId);

        if (!result) return NotFound();

        return NoContent();

    }

}