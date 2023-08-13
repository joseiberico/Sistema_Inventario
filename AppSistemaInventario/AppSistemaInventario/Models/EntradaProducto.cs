using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppSistemaInventario.Models;

public partial class EntradaProducto
{
    public int Id { get; set; }

    public int? Idproducto { get; set; }

    public string? Descripcion { get; set; }

    public int? StockE { get; set; }

    public int? IdUsuario { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
    [JsonIgnore]
    public virtual Producto? IdproductoNavigation { get; set; }
}
