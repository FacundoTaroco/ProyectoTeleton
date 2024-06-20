using LogicaNegocio.DTO;
using LogicaNegocio.EntidadesWit;
using LogicaNegocio.EntidadesWit.Entrenamiento;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections;
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
        private string Version = "20240620";




        //HACER TODO ASINCRONO ACA

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




        public IEnumerable<Intent> GetIntent()
        {
            var options = new RestClientOptions(linkAPI);
            var client = new RestClient(options);
            var request = new RestRequest("/intents?v="+Version, Method.Get);
            request.AddHeader("Authorization", $"Bearer {Token}");
            RestResponse response = client.ExecuteGet(request);
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
                var intents = JsonSerializer.Deserialize<List<Intent>>(response.Content, optionsJson);
                return intents;
            }
            else
            {
                Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                throw new Exception("Error " + error.Code + " " + error.Details);
            }
        }


        public Intent PostIntent(IntentDTO intentDto)
        {
            var options = new RestClientOptions(linkAPI);

            var client = new RestClient(options);
            var request = new RestRequest("/intents?v=" + Version, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {Token}");
            request.AddJsonBody(intentDto);
            RestResponse response = client.ExecutePost(request);


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
                var intent = JsonSerializer.Deserialize<Intent>(response.Content, optionsJson);
                return intent;
            }
            else
            {
                Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                throw new Exception("Error " + error.Code + " " + error.Details);
            }
        }

        public void PostUtterance(List<UtteranceDTO> utterances)
        {
            var options = new RestClientOptions(linkAPI);
            var client = new RestClient(options);
            var request = new RestRequest("/utterances?v=" + Version, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {Token}");

            JsonSerializerOptions optionsJson = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string jsonBody = JsonSerializer.Serialize(utterances, optionsJson);

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
              
            }
            else
            {
                Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                throw new Exception("Error " + error.Code + " " + error.Details);
            }
        }

    }
}
