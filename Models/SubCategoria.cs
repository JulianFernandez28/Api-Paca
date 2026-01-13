using System.Security.Principal;

namespace Api_GestionVentas.Models
{
    public class SubCategoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int CategoriaId { get; set; }

        public bool EsActivo { get; set; }
        public Categoria Categoria { get; set; }
    }
}
