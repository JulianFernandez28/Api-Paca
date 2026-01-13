using Api_GestionVentas.Models;

namespace Api_GestionVentas.Services.IServices
{
    public interface IAuthService
    {
        string GenerateToken(Usuario user);
        bool VerifyPassword(string plainPassword, string hashedPassword);
    }
}
