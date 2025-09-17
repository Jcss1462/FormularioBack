using System;
using System.Collections.Generic;

namespace FormularioBack.Models;

public partial class Respuesta
{
    public int IdRespuesta { get; set; }

    public int IdFormulario { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public virtual Formulario IdFormularioNavigation { get; set; } = null!;

    public virtual ICollection<RespuestasPregunta> RespuestasPregunta { get; set; } = new List<RespuestasPregunta>();
}
