using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.DTOs.Categoria
{
    public class CategoriaUpdateDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool EsActivo { get; set; }
    }
}
