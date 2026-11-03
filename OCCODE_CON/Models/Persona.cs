using System;
using System.Collections.Generic;

namespace OCCODE_CON.Models;

/// <summary>
/// Guarda la información física/personal de la persona.
/// </summary>
public partial class Persona
{
    /// <summary>
    /// Identificador único de la persona
    /// </summary>
    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    /// <summary>
    /// Documento Personal de Identificación único
    /// </summary>
    public string Dpi { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
