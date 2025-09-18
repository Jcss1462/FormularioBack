namespace FormularioBack.Dtos
{
    public class FormularioResumenDto
    {
        public int IdFormulario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int CantidadRespuestas { get; set; }
        public double PuntajePromedio { get; set; }
    }
}
