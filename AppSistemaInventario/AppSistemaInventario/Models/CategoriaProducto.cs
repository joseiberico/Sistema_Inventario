using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppSistemaInventario.Models;

public partial class CategoriaProducto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Es necesario insertar una categoria")]
    public string Tipo { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
