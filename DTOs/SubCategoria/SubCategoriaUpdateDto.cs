namespace Api_GestionVentas.DTOs.SubCategoria
{
    public class SubCategoriaUpdateDto
    {
        public string Nombre { get; set; }
        public int CategoriaId { get; set; }
        public bool EsActivo { get; set; }
    }
}
