using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Servicios
{
    public class SolicitarEncuestasService
    {
        private readonly IConfiguration _config;
        private IRepositorioEncuesta _repositorioEncuesta;
        public SolicitarEncuestasService(IConfiguration config, IRepositorioEncuesta repositorioEncuesta)
        {
            _config = config;
            _repositorioEncuesta = repositorioEncuesta;

        }

        public async Task<IEnumerable<Encuesta>> ObtenerEncuestas()
        {

            //SimuladorServidorCentral
            var connectionString = _config["ConnectionStrings:SimuladorServidorCentral"];
            var commandText = $"SELECT * FROM GetEncuestas()";
            using (SqlConnection con = new(connectionString))
            {
                try
                {
                    List<Encuesta> encuestas = new List<Encuesta>();
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {
                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                int satisfaccionGeneral = reader.GetInt32(1);
                                string comentarios = reader.GetString(2);

                                Encuesta encuesta = new Encuesta(id, satisfaccionGeneral, comentarios);
                                encuestas.Add(encuesta);
                            }
                        }
                    }
                    return encuestas;
                }
                catch (Exception e)
                {
                    throw new TeletonServerException("Error de conexión con el servidor central" + e.Message);
                }
            }
        }

        public async Task<IEnumerable<Encuesta>> ObtenerEncuestasPorCedula(string cedula)
        {
            var connectionString = _config["ConnectionStrings:SimuladorServidorCentral"];
            var commandText = $"SELECT * FROM GetEncuestasPorCedula(@Cedula)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    List<Encuesta> encuestas = new List<Encuesta>();
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {
                        cmd.Parameters.AddWithValue("@Cedula", cedula);

                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                int satisfaccionGeneral = reader.GetInt32(1);
                                string comentarios = reader.GetString(2);

                                Encuesta encuesta = new Encuesta(id,satisfaccionGeneral, comentarios);
                                encuestas.Add(encuesta);
                            }
                        }
                    }
                    return encuestas;
                }
                catch (Exception e)
                {
                    throw new TeletonServerException("Error de conexión con el servidor central" + e.Message);
                }
            }
        }
    }
}
