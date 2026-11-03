using System;
using System.Collections.Generic;

namespace OCCODE_CON.Models;

/// <summary>
/// Almacena las credenciales y accesos de los usuarios del sistema.
/// </summary>
public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdPersona { get; set; }

    public int IdPerfil { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// 1 Activo, 0 Inactivo
    /// </summary>
    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Perfil IdPerfilNavigation { get; set; } = null!;

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
