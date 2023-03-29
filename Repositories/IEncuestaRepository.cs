using PruebaNet.Models;

namespace PruebaNet.Repositories
{
    public interface IEncuestaRepository
    {
        public Task<IEnumerable<Encuesta>> GetEncuestas();

        public Task<IEnumerable<RespuestaEncuesta>> GetRespuestaByEncuestaId(int encuestaId);

        public Task<int> GuardarEncuesta();

        public Task GuardarRespuestas(List<RespuestaEncuesta> respuestas, int encuestaId);

    }
}
