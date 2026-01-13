using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.Models
{
    public class Venta
    {
        [Key]

        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public decimal Total { get; set; }
        public int EmpresaId
        {
            get; set;
        }

        public ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
