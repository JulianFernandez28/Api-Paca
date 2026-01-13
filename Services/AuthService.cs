using Api_GestionVentas.Models;
using Api_GestionVentas.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_GestionVentas.Services
{
    public class AuthService : IAuthService

    {

        private readonly IConfiguration _config;


        public AuthService(IConfiguration config)

        {

            _config = config;

        }


        public string GenerateToken(Usuario user)

        {

            var claims = new[]
            {

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                new Claim(ClaimTypes.Email, user.Email),

                new Claim(ClaimTypes.Role, user.Rol),
                new Claim("EmpresaId", user.EmpresaId.ToString())

            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken
             (

                issuer: _config["Jwt:Issuer"],

                audience: _config["Jwt:Audience"],

                claims: claims,

                expires: DateTime.Now.AddHours(1),

                signingCredentials: creds
             );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public bool VerifyPassword(string plainPassword, string hashedPassword)

        {

            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);

        }

    }
}
