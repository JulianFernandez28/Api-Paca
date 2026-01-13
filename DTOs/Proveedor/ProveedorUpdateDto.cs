using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.DTOs.Proveedor
{
    public class ProveedorUpdateDto
    {
        
        public string Nombre { get; set; }
        
        public string Contacto { get; set; } = string.Empty;
        
        public string Direccion { get; set; } = string.Empty;

        public bool EsActivo { get; set; }
    }
}
