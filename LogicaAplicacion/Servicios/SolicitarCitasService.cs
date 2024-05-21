using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.Servicios
{
    public class SolicitarCitasService
    {
        public string linkAPI { get; set; }
        public SolicitarCitasService()
        {
            linkAPI = "https://localhost:7201/";
        }
        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitas() {
            try
            {
                var options = new RestClientOptions(linkAPI)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("api/Cita", Method.Get);
                /*request.AddHeader("Authorization", $"Bearer {token}");*/
                RestResponse response = await client.ExecuteGetAsync(request);
                JsonSerializerOptions optionsJson = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                if (response.Content == null)
                {
                    throw new Exception("Error de comunicacion con la api");
                }
                HttpStatusCode res = response.StatusCode;
                if (res == HttpStatusCode.OK)
                {
                    var Citas = JsonSerializer.Deserialize<List<CitaMedicaDTO>>(response.Content, optionsJson);
                    return Citas;
                }
                else
                {
                    Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                    throw new Exception("Error " + error.Code + " " + error.Details);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasPorCedula(string cedula)
        {
            try
            {
                var options = new RestClientOptions(linkAPI)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("api/Cita/GetPorCedula/"+cedula, Method.Get);
                /*request.AddHeader("Authorization", $"Bearer {token}");*/
                RestResponse response = await client.ExecuteGetAsync(request);
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
                    var Citas = JsonSerializer.Deserialize<List<CitaMedicaDTO>>(response.Content, optionsJson);
                    return Citas;
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



