using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.Models
{
    public class Producto
    {
        [Key]

        public int Id { get; set; }
        [Required]

        public string Codigo { get; set; }


        [Required]

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool EsActivo { get; set; }

        [Required]

        public decimal Precio { get; set; }

        [Required]

        public int Stock { get; set; }



        // FKs

        [ForeignKey("Proveedor")]

        public int IdProveedor { get; set; }

        public Proveedor Proveedor { get; set; }



        [ForeignKey("Subcategoria")]

        public int IdSubcategoria { get; set; }

        public SubCategoria Subcategoria { get; set; }



        // Relación con Detalles_Venta

        public ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
