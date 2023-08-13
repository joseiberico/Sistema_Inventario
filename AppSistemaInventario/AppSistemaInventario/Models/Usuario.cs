using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppSistemaInventario.Models;

public class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public byte? Estado { get; set; }
    [JsonIgnore]
    public virtual ICollection<EntradaProducto> EntradaProductos { get; set; } = new List<EntradaProducto>();
    [JsonIgnore]
    public virtual ICollection<SalidaProducto> SalidaProductos { get; set; } = new List<SalidaProducto>();
}
