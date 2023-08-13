using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiSistemaInventario.Models;

public partial class EntradaProducto
{
    public int Id { get; set; }

    public int? Idproducto { get; set; }

    public string? Descripcion { get; set; }

    public int? StockE { get; set; }

    public int? IdUsuario { get; set; }
    
    public virtual Usuario? IdUsuarioNavigation { get; set; }
    
    public virtual Producto? IdproductoNavigation { get; set; }
}
