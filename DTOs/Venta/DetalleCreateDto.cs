namespace Api_GestionVentas.DTOs.Venta
{
    public class DetalleCreateDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}
