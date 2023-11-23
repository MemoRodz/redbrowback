using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedBrowBack.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    [JsonIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
