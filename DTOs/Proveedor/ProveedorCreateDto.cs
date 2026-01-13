using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.DTOs.Proveedor
{
    public class ProveedorCreateDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Contacto { get; set; } = string.Empty;
        [Required]
        public string Direccion { get; set; } = string.Empty;
    }
}
