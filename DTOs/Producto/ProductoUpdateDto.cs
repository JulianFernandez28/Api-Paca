using System.ComponentModel.DataAnnotations;

namespace Api_GestionVentas.DTOs.Producto
{
    public class ProductoUpdateDto
    {
        
        public string Codigo { get; set; }
        
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        public decimal Precio { get; set; }
        
        public int Stock { get; set; }
        
        public int IdProveedor { get; set; }
        
        public int IdSubcategoria { get; set; }

        public bool EsActivo { get; set; }
    }
}
