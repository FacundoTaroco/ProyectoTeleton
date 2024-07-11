using LogicaNegocio.DTO;
using LogicaNegocio.EntidadesWit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LogicaAplicacion.Servicios
{
    public class GeolocalizacionService
    {

        public static string linkAPI = "https://geocode.maps.co";

        public GeolocalizacionService() { }


        public string ObtenerDireccionTeleton() {


            return "Av Carlos Brussa 2854";
            
        }
        public CoordenadasDTO ObtenerCoordenadasTeleton() {


            return new CoordenadasDTO("-34.85813539078298", "-56.210354046871146");
        }


        public CoordenadasDTO ObtenerCoordenadas(string direccion)
        {
            direccion = direccion + ",Uruguay";
           
                var options = new RestClientOptions(linkAPI);
                var client = new RestClient(options); 
                var request = new RestRequest("search?q=" +direccion+ "&api_key=66902823ec72b421928410dbj027f7c", Method.Post);
              
                JsonSerializerOptions optionsJson = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };


                RestResponse response = client.ExecutePost(request);
                if (response.Content == null)
                {
                    throw new Exception("Error de comunicación con la API");//ESTO EXPLOTA SI LLEGA
                }

                HttpStatusCode res = response.StatusCode;
                if (res == HttpStatusCode.OK || res == HttpStatusCode.Created)
                {
                List<CoordenadasDTO> coordenadas = new List<CoordenadasDTO>();
                    CoordenadasDTO coords = new CoordenadasDTO();
                coordenadas = JsonSerializer.Deserialize<List<CoordenadasDTO>>(response.Content, optionsJson);

                coords = coordenadas.FirstOrDefault();
                return coords;
                }
                else
                {
                    Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                    throw new Exception("Error " + error.Code + " " + error.Details);//ESTO EXPLOTA SI LLEGA
                }
            }



        

    }
}
