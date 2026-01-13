using Api_GestionVentas.Data;
using Api_GestionVentas.Models;
using Api_GestionVentas.Services;
using Api_GestionVentas.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_GestionVentas.DTOs;
namespace Api_GestionVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(AppDbContext context,IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // POST: api/auth/register

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)

        {

            // Validar si el email ya existe

            if (await _context.Usuarios.AnyAsync(u => u.Email == registerDto.Email))

            {

                return BadRequest(new { Message = "El email ya está registrado." });

            }


            // Crear usuario

            var user = new Usuario

            {

                Nombre = registerDto.Nombre,

                Email = registerDto.Email,

                ContraseniaHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password), // Hash seguro

                Rol = "vendedor" // Rol por defecto; ajusta si necesitas admin

            };


            _context.Usuarios.Add(user);

            await _context.SaveChangesAsync();


            // Opcional: Generar token inmediatamente después del registro

            var token = _authService.GenerateToken(user);


            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new { User = user, Token = token });

        }


        // GET: api/auth/user/5 (para referencia, no obligatorio)

        [HttpGet("user/{id}")]

        public async Task<IActionResult> GetUser(int id)

        {

            var user = await _context.Usuarios.FindAsync(id);

            if (user == null) return NotFound();

            return Ok(user);

        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            
            var user = await _context.Usuarios.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Email == dto.Email && u.EsActivo ==true);

            if (user == null || !_authService.VerifyPassword(dto.Password, user.ContraseniaHash)) return Unauthorized();



            var token = _authService.GenerateToken(user);

            return Ok(new { Token = token });

        }

       
    }
}
