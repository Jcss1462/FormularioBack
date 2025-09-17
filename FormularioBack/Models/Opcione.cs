using System;
using System.Collections.Generic;

namespace FormularioBack.Models;

public partial class Opcione
{
    public int IdOpcion { get; set; }

    public int IdPregunta { get; set; }

    public string Texto { get; set; } = null!;

    public bool Correcta { get; set; }

    public virtual Pregunta IdPreguntaNavigation { get; set; } = null!;

    public virtual ICollection<RespuestasPregunta> RespuestasPregunta { get; set; } = new List<RespuestasPregunta>();
}
