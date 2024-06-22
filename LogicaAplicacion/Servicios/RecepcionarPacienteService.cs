using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.Servicios
{
    public class RecepcionarPacienteService
    {
        private readonly IConfiguration _config;
        public RecepcionarPacienteService(IConfiguration config)
        {
            _config = config;
        }



        public async void RecepcionarPaciente(int pkAgenda) {

            try
            {
                var connectionString = _config["ConnectionStrings:SimuladorServidorCentral"];
                var commandText = "RecepcionarPaciente";
                // Establece la conexión
                List<CitaMedicaDTO> citasMedicas = new List<CitaMedicaDTO>();
                using (SqlConnection con = new(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@pkAgenda", pkAgenda));


                        con.Open();
                        await cmd.ExecuteNonQueryAsync();
                        con.Close();

                    }
                }
            }
            catch (Exception)
            {

                throw;
                //ACA VER QUE HACER SI NO SE PUEDE GENERAR EL AVISO REQUERIMIENTO RF13 (Recepción Automatizada de Usuarios)
            }


        }

        /*
        public async void GenerarAviso(AvisoMedicoDTO aviso) {

            try
            {
                var options = new RestClientOptions(linkAPI)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("api/Aviso", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(aviso);
                RestResponse response = await client.ExecutePostAsync(request);

                JsonSerializerOptions optionsJson = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                if (response.Content == null)
                {
                    throw new ApiErrorException("Error de comunicacion con la api");
                }
                HttpStatusCode res = response.StatusCode;
                if (res != HttpStatusCode.Created)
                {
                    Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                    throw new ApiErrorException("Error " + error.Code + " " + error.Details);
                }
               
            }
            catch (ApiErrorException)
            {
                //ACA VER QUE HACER SI NO SE PUEDE GENERAR EL AVISO REQUERIMIENTO RF13 (Recepción Automatizada de Usuarios)
                //no podemos tirar excepciones porque nos tranca la accion del controller de totem por lo que los diferentes avisos se tienen que guardar
                //en una pila de llamados que se envian al servidor una vez que este se encuentra disponible con cierta politica de reintento

                Console.WriteLine("No se mando el aviso porque no se encuentra disponible el servidor central");
            }
            catch (Exception)
            {
             //ACA VER QUE HACER SI NO SE PUEDE GENERAR EL AVISO REQUERIMIENTO RF13 (Recepción Automatizada de Usuarios)
                //no podemos tirar excepciones porque nos tranca la accion del controller de totem por lo que los diferentes avisos se tienen que guardar
                //en una pila de llamados que se envian al servidor una vez que este se encuentra disponible con cierta politica de reintento

                Console.WriteLine("No se mando el aviso porque no se encuentra disponible el servidor central");
            }
        
         
        }*/
    }
}
