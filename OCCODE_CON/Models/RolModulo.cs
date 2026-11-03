using System;
using System.Collections.Generic;

namespace OCCODE_CON.Models;

public partial class RolModulo
{
    public int IdRol { get; set; }

    public int IdModulo { get; set; }

    public bool? PuedeCrear { get; set; }

    public bool? PuedeLeer { get; set; }

    public bool? PuedeEditar { get; set; }

    public bool? PuedeEliminar { get; set; }

    public virtual Modulo IdModuloNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
