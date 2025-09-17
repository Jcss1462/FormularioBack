using System;
using System.Collections.Generic;

namespace FormularioBack.Models;

public partial class FormularioHasPregunta
{
    public int IdFhp { get; set; }

    public int IdFormulario { get; set; }

    public int IdPregunta { get; set; }

    public virtual Formulario IdFormularioNavigation { get; set; } = null!;

    public virtual Pregunta IdPreguntaNavigation { get; set; } = null!;
}
