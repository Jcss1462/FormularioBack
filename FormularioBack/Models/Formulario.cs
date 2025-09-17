using System;
using System.Collections.Generic;

namespace FormularioBack.Models;

public partial class Formulario
{
    public int IdFormulario { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<FormularioHasPregunta> FormularioHasPregunta { get; set; } = new List<FormularioHasPregunta>();

    public virtual ICollection<Respuesta> Respuesta { get; set; } = new List<Respuesta>();
}
