using LogicaNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Text.Json;
using System.Net;
using LogicaAplicacion.Excepciones;

namespace LogicaAplicacion.Servicios


{
    //esta clase es la encargada de solicitar el listado de pacientes del servidor de teleton para luego enviarselo a ActualizarPacientes
    public class SolicitarPacientesService
    {
        public string linkAPI { get; set; }
        public SolicitarPacientesService() {
            linkAPI = "https://localhost:7201/";
        }


        //metodo que hace el fetch a la api con los pacientes
        public async Task<IEnumerable<PacienteDTO>> solicitarPacientesATeleton() {
            try
            {
                var options = new RestClientOptions(linkAPI)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(linkAPI + "api/Paciente", Method.Get);
                //autorizacion?
                RestResponse response = await client.ExecuteGetAsync<IEnumerable<PacienteDTO>>(request);
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

                if (res == HttpStatusCode.OK)
                {
                    var pacientes = JsonSerializer.Deserialize<List<PacienteDTO>>(response.Content, optionsJson);
                    return pacientes;
                }
                else
                {
                    Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                    throw new ApiErrorException("Error " + error.Code + " " + error.Details);
                }
            }
            catch (ApiErrorException) {

                throw;
            }
            catch (Exception)
            {
                throw;
            }

           


        }

    }

}
