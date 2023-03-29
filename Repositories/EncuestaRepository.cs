using Microsoft.Extensions.Options;
using PruebaNet.Configuration;
using PruebaNet.Models;
using System.Data.SqlClient;

namespace PruebaNet.Repositories
{
    public class EncuestaRepository : IEncuestaRepository
    {
        private readonly ConfiguracionConexionDB _connectionString;
        public EncuestaRepository(IOptions<ConfiguracionConexionDB> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task<IEnumerable<Encuesta>> GetEncuestas()
        {

            var encuestas = new List<Encuesta>();
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {

                cnn.Open();
                string query = "select * from Encuestas";
                SqlCommand command = new SqlCommand(query, cnn);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var encuesta = new Encuesta()
                    {
                        EncuestaId = reader.GetInt32(0),
                        Fecha = reader.GetDateTime(1)

                    };

                    encuestas.Add(encuesta);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return encuestas;
        }

        public async Task<IEnumerable<RespuestaEncuesta>> GetRespuestaByEncuestaId(int encuestaId)
        {
            var respuestas = new List<RespuestaEncuesta>();
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {

                cnn.Open();
                string query = "select * from Respuestas where EncuestaId = @encuestaId";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@encuestaId", encuestaId);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var respuesta = new RespuestaEncuesta()
                    {
                        Pregunta = reader.GetString(1),
                        Respuesta = reader.GetString(2)

                    };

                    respuestas.Add(respuesta);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return respuestas;
        }

        public async Task<int> GuardarEncuesta()
        {
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();                
                string queryEncuesta = "insert into Encuestas values(@fecha)"; 
                SqlCommand cmd = new SqlCommand(queryEncuesta, cnn);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();
                string queryMaxId = "select max(EncuestaId) from Encuestas";
                cmd.CommandText = queryMaxId;
                await cmd.ExecuteNonQueryAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                int encuestaId = 0;
                while (reader.Read())
                {
                    encuestaId = reader.GetInt32(0);                    
                }
                
                return encuestaId;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return 0;
        }


		public async Task GuardarRespuestas(List<RespuestaEncuesta> respuestas, int encuestaId)
		{
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();               
                foreach (var item in respuestas)
				{
                    string queryRespuestas = "insert into Respuestas values(@pregunta, @respuesta, @encuestaId)";
                    SqlCommand command = new SqlCommand(queryRespuestas, cnn);
                    command.Parameters.AddWithValue("@pregunta", item.Pregunta);
                    command.Parameters.AddWithValue("@respuesta", item.Respuesta);
                    command.Parameters.AddWithValue("@encuestaId", encuestaId);
                    await command.ExecuteNonQueryAsync();
                }                             
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

	}
}
