using System;
using System.Collections.Generic;

namespace FormularioBack.Models;

public partial class Pregunta
{
    public int IdPregunta { get; set; }

    public string Pregunta1 { get; set; } = null!;

    public virtual ICollection<FormularioHasPregunta> FormularioHasPregunta { get; set; } = new List<FormularioHasPregunta>();

    public virtual ICollection<Opcione> Opciones { get; set; } = new List<Opcione>();

    public virtual ICollection<RespuestasPregunta> RespuestasPregunta { get; set; } = new List<RespuestasPregunta>();
}
