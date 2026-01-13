namespace Api_GestionVentas.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public bool EsActivo { get; set; }
        public int EmpresaId
        {
            get; set;
        }
    }
}
