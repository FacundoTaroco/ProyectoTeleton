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
    public class GenerarAvisoMedicoService
    {
        public string linkAPI { get; set; }
        public GenerarAvisoMedicoService()
        {
            linkAPI = "https://localhost:7201/";
        }

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

         
        }
    }
}
