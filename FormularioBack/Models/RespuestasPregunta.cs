using System;
using System.Collections.Generic;

namespace FormularioBack.Models;

public partial class RespuestasPregunta
{
    public int IdRp { get; set; }

    public int IdRespuesta { get; set; }

    public int IdPregunta { get; set; }

    public int? IdOpcionSeleccionada { get; set; }

    public virtual Opcione? IdOpcionSeleccionadaNavigation { get; set; }

    public virtual Pregunta IdPreguntaNavigation { get; set; } = null!;

    public virtual Respuesta IdRespuestaNavigation { get; set; } = null!;
}
