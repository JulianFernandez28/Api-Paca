using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.Models
{
    public class Usuario
    {
        [Key]

        public int Id { get; set; }

        [Required]

        public string Nombre { get; set; }

        [Required, EmailAddress]

        public string Email { get; set; }
        public bool EsActivo { get; set; }

        [Required]

        public string ContraseniaHash { get; set; }

        [Required]

        public string Rol { get; set; } // 'admin', 'vendedor'
        public int EmpresaId { get; set; }



        // Relación con Ventas

        public ICollection<Venta> Ventas { get; set; }
    }
}
