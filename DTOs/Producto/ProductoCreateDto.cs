using Api_GestionVentas.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.DTOs.Producto
{
    public class ProductoCreateDto
    {
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int IdProveedor { get; set; }
        [Required]
        public int IdSubcategoria { get; set; }
    }
}
