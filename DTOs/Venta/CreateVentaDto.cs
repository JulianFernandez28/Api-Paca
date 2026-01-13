using Api_GestionVentas.Models;

namespace Api_GestionVentas.DTOs.Venta
{
    public class CreateVentaDto
    {
        public DateTime Fecha { get; set; } = DateTime.Now;

        public int IdUsuario { get; set; }

        public decimal Total { get; set; }

        public ICollection<DetalleCreateDto> Detalles { get; set; }
    }
}
