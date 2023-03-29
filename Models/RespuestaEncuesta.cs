namespace PruebaNet.Models
{
    public class RespuestaEncuesta
    {
        public int RespuestaId { get; set; }
        public string Pregunta { get; set; } = string.Empty;
        public string Respuesta { get; set; } = string.Empty;
    }
}
