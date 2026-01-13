using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.DTOs.Categoria
{
    public class CategoriaCreateDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}
