using System;
using System.Collections.Generic;

namespace OCCODE_CON.Models;

/// <summary>
/// Define el nivel o cargo de acceso.
/// </summary>
public partial class Rol
{
    public int IdRol { get; set; }

    /// <summary>
    /// Ej. ADMIN, EDITOR, LECTOR
    /// </summary>
    public string NombreRol { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<RolModulo> RolModulos { get; set; } = new List<RolModulo>();

    public virtual ICollection<Usuario> IdUsuarios { get; set; } = new List<Usuario>();
}
