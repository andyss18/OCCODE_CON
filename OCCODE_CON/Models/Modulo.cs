using System;
using System.Collections.Generic;

namespace OCCODE_CON.Models;

/// <summary>
/// Representa la pantalla, vista o recurso dentro de la aplicación.
/// </summary>
public partial class Modulo
{
    public int IdModulo { get; set; }

    public string NombreModulo { get; set; } = null!;

    /// <summary>
    /// FK opcional para menús desplegables o submenús
    /// </summary>
    public int? IdModuloPadre { get; set; }

    public virtual Modulo? IdModuloPadreNavigation { get; set; }

    public virtual ICollection<Modulo> InverseIdModuloPadreNavigation { get; set; } = new List<Modulo>();

    public virtual ICollection<RolModulo> RolModulos { get; set; } = new List<RolModulo>();
}
