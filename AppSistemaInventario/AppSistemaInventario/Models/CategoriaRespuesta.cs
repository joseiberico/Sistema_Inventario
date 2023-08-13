namespace AppSistemaInventario.Models
{
    public class CategoriaRespuesta
    {
        public string mensaje { get; set; }
        public List<CategoriaProducto> lista { get; set; }
        public CategoriaProducto objeto { get; set; }
    }
}
