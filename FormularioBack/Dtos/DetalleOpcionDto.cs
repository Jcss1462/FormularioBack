namespace FormularioBack.Dtos
{
    public class DetalleOpcionDto
    {
        public int IdOpcion { get; set; }
        public string Texto { get; set; } = string.Empty;
        public bool Correcta { get; set; }
    }
}
