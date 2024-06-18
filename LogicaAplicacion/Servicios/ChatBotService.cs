using LogicaNegocio.DTO;
using LogicaNegocio.EntidadesWit;
using Microsoft.Extensions.Logging;
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
    public class ChatBotService
    {
        private string linkAPI = "https://api.wit.ai";
        private string Token = "CCQUHLZIFMAQFNZIB3BQW4H2L7NO2DL4";

     
        public Evento PostEvent(MensajeBotDTO msj)
        {
            var options = new RestClientOptions(linkAPI);
            var client = new RestClient(options);
            var request = new RestRequest("/event?v=20240618&session_id=prodn7i&context_map=%7B%7D", Method.Post);
            //request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {Token}");

            JsonSerializerOptions optionsJson = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string jsonBody = JsonSerializer.Serialize(msj, optionsJson);

            request.AddStringBody(jsonBody, DataFormat.Json);

            RestResponse response = client.ExecutePost(request);

            Console.WriteLine("Response Content:");
            Console.WriteLine(response.Content);

            if (response.Content == null)
            {
                throw new Exception("Error de comunicación con la API");
            }

            HttpStatusCode res = response.StatusCode;
            if (res == HttpStatusCode.OK || res == HttpStatusCode.Created)
            {
                var evento = JsonSerializer.Deserialize<Evento>(response.Content, optionsJson);
                return evento;
            }
            else
            {
                Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                throw new Exception("Error " + error.Code + " " + error.Details);
            }
        }
        public string Responder(string mensaje) {


            return "Mensaje de bot";
        }

    }
}
