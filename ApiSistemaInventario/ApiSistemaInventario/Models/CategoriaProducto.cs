using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiSistemaInventario.Models;

public partial class CategoriaProducto
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
