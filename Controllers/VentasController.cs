using Api_GestionVentas.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_GestionVentas.Services.IServices;
using Api_GestionVentas.DTOs.Venta;

namespace Api_GestionVentas.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    [Authorize]

    public class VentasController : ControllerBase

    {

        private readonly IVentaService _ventaService;


        public VentasController(IVentaService ventaService)

        {

            _ventaService = ventaService;

        }


        // GET: api/ventas

        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            var ventas = await _ventaService.GetAllAsync();

            return Ok(ventas);

        }


        // GET: api/ventas/5

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)

        {

            var venta = await _ventaService.GetByIdAsync(id);

            if (venta == null) return NotFound();

            return Ok(venta);

        }


        // POST: api/ventas

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateVentaDto venta)

        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            // Obtener idUsuario del token

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null) return Unauthorized();

            int idUsuario = int.Parse(userIdClaim.Value);


            try

            {
                Venta ventaNueva = new Venta();
                ventaNueva.DetalleVenta = new List<DetalleVenta>();


                foreach (var detalleNuevo in venta.Detalles)
                {
                    DetalleVenta  detalle = new DetalleVenta();
                    detalle.ProductoId = detalleNuevo.ProductoId;
                    detalle.Cantidad = detalleNuevo.Cantidad;
                    detalle.PrecioUnitario = detalle.PrecioUnitario;

                    ventaNueva.DetalleVenta.Add(detalle);
                }

                var nuevaVenta = await _ventaService.CreateAsync(ventaNueva, idUsuario);

                return CreatedAtAction(nameof(GetById), new { id = nuevaVenta.Id }, nuevaVenta);

            }

            catch (InvalidOperationException ex)

            {

                return BadRequest(new { Message = ex.Message });

            }

        }

    }
}
