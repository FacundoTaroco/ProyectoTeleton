using LogicaAplicacion.CasosUso.PreguntasFrecCU;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesWit;
using LogicaNegocio.EntidadesWit.Entrenamiento;
using LogicaNegocio.EntidadesWit.GetMessage;
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
using System.Web;

namespace LogicaAplicacion.Servicios
{
    public class ChatBotService
    {
        private string linkAPI = "https://api.wit.ai";
        private string Token = "CCQUHLZIFMAQFNZIB3BQW4H2L7NO2DL4";
        private string Version = "20240620";

        private GetPreguntasFrec _getPreguntasFrec;
        private GeolocalizacionService _geolocalizacionService;


        public ChatBotService(GetPreguntasFrec getPreguntasFrec, GeolocalizacionService geolocalizacion) { 
            _getPreguntasFrec = getPreguntasFrec;
            _geolocalizacionService = geolocalizacion;
        }



        public string Responder(string mensaje)
        {

            MensajeRespuesta mensajeGetMessage = GetMessage(mensaje);


            if (mensajeGetMessage.Intents.Count() > 0)
            {
                //aca supo responder el chatbot 
                string intent = mensajeGetMessage.Intents[0].name;

                if (intent.Equals("transporte")) {

                    //respuesta especial donde tenemos que enviar un link que le muestre las direcciones que tiene que seguir

                    string ubicacionInicial = mensajeGetMessage.Entities.First().Value[0].Value;
                    string linkTransporte = GenerarLinkTransporte(ubicacionInicial);

                    return "<a href='" + linkTransporte + "' target='_blank'> Presione este mensaje para ver las indicaciones</a>";

                  
                
                }


                CategoriaPregunta categoria = _getPreguntasFrec.GetCategoriaPorNombre(intent);
                return categoria.Respuesta;
            }
            else { 
                //aca no supo responder el chatbot
            return "Reescriba la pregunta, por favor.";
            
            }
         
        }


        public string GenerarLinkTransporte(string puntoPartida) { 
            
            CoordenadasDTO coordenadasPartida = _geolocalizacionService.ObtenerCoordenadas(puntoPartida);
            CoordenadasDTO coordenadasLLegada = _geolocalizacionService.ObtenerCoordenadasTeleton();

            string partida = puntoPartida;
            string llegada = _geolocalizacionService.ObtenerDireccionTeleton();


            partida = partida.Replace(" ", "%20");
            llegada = llegada.Replace(" ", "%20");

            string link = $"https://moovitapp.com/montevideo-1672/poi/{llegada}/{partida}/es?fll={coordenadasLLegada.lat}_{coordenadasLLegada.lon}&tll={coordenadasPartida.lat}_{coordenadasPartida.lon}";


            return link;
        }



        //HACER TODO ASINCRONO ACA


        public MensajeRespuesta GetMessage(string input) {


            var options = new RestClientOptions(linkAPI);
            var client = new RestClient(options);
            var request = new RestRequest("/message?v=" + Version + "&q=" + input, Method.Get);
            request.AddHeader("Authorization", $"Bearer {Token}");
            RestResponse response = client.ExecuteGet(request);
            JsonSerializerOptions optionsJson = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            if (response.Content == null)
            {
                throw new Exception("Error de comunicacion con la api");//ESTO EXPLOTA SI LLEGA
            }
            HttpStatusCode res = response.StatusCode;
            if (res == HttpStatusCode.OK)
            {
                var mensaje = JsonSerializer.Deserialize<MensajeRespuesta>(response.Content, optionsJson);
                return mensaje;
            }
            else
            {
                Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                throw new Exception("Error " + error.Code + " " + error.Details);
            }



        }

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
                throw new Exception("Error de comunicación con la API");//ESTO EXPLOTA SI LLEGA
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
                throw new Exception("Error " + error.Code + " " + error.Details);//ESTO EXPLOTA SI LLEGA
            }
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
                throw new Exception("Error de comunicacion con la api");//ESTO EXPLOTA SI LLEGA
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
                throw new Exception("Error de comunicacion con la api"); //ESTO EXPLOTA SI LLEGA
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
                throw new Exception("Error " + error.Code + " " + error.Details);//ESTO EXPLOTA SI LLEGA
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
              //ACA LLEGA LA RESPUESTA BOOL DE SI SE ENTRENO VER QUE HACER DESPUES CON ESO
            }
            else
            {
                Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                throw new Exception("Error " + error.Code + " " + error.Details);//ESTO EXPLOTA SI LLEGA
            }
        }

    }
}
