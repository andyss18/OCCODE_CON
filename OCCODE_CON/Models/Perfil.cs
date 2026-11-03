using System;
using System.Collections.Generic;

namespace OCCODE_CON.Models;

/// <summary>
/// Define el perfil o categoría general del usuario.
/// </summary>
public partial class Perfil
{
    public int IdPerfil { get; set; }

    /// <summary>
    /// Ej. Administrativo, Cliente VIP, Soporte Técnico
    /// </summary>
    public string NombrePerfil { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
