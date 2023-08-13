using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiSistemaInventario.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public string? Marca { get; set; }

    public int? Idcategoria { get; set; }

    [JsonIgnore]
    public virtual ICollection<EntradaProducto> EntradaProductos { get; set; } = new List<EntradaProducto>();

    public virtual CategoriaProducto? IdcategoriaNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<SalidaProducto> SalidaProductos { get; set; } = new List<SalidaProducto>();
}
