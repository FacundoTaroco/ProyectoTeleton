using LogicaNegocio.DTO;
using LogicaNegocio.EntidadesWit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LogicaAplicacion.Servicios
{
    //Servicio para obtener coordenadas a partir de direcciones
    public class GeolocalizacionService
    {

        public static string linkAPI = "https://geocode.maps.co";

        public GeolocalizacionService() { }





        public string ObtenerDireccionInterBelloni()
        {

            return "Avenida 8 de octubre 4849";

        }

        public CoordenadasDTO ObtenerCoordenadasInterBelloni()
        {

            return new CoordenadasDTO("-34.85404736136892", "-56.132178439611785");

        }



        public string ObtenerDireccionRioBranco()
        {

            return "Galicia 911";

        }


        public CoordenadasDTO ObtenerCoordenadasRioBranco()
        {

            return new CoordenadasDTO("-34.90048675327727", "-56.196533408925674");

        }


        public string ObtenerDireccionTresCruces() {

            return "Bulevar Artigas 1825";

        }


        public CoordenadasDTO ObtenerCoordenadasTresCruces() {

            return new CoordenadasDTO("-34.8938121076451", "-56.166352808891396");

        }

        public string ObtenerDireccionTeleton() {


            return "Av Carlos Brussa 2854";

        }
        public CoordenadasDTO ObtenerCoordenadasTeleton() {


            return new CoordenadasDTO("-34.85813539078298", "-56.210354046871146");
        }


        public CoordenadasDTO ObtenerCoordenadas(string direccion)
        {
            direccion = direccion + ", Montevideo Uruguay";

            var options = new RestClientOptions(linkAPI);
            var client = new RestClient(options);
            var request = new RestRequest("search?q=" + direccion + "&api_key=66902823ec72b421928410dbj027f7c", Method.Post);

            JsonSerializerOptions optionsJson = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };


            RestResponse response = client.ExecutePost(request);
            if (response.Content == null)
            {
                throw new Exception("Error de comunicación con la API");
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
   
        //Esta funcion usa la formula matematica de Haversine para calcular la distancia entre las coordenadas dadas y las coordenadas de montevideo, si esta distancia es mayor
        //a 15km(aprox el radio de montevideo) entoncer retorna false(No son de montevideo las coordenadas)
        public bool EsDeMontevideo(CoordenadasDTO coordenadas) {

            if (coordenadas == null) {

                return false;
            
            
            }

            double montevideoLat = -34.9011;
            double montevideoLon = -56.1645;
            double radioMontevideo = 15; 

            double distanciaAMontevideo = Haversine(Double.Parse(coordenadas.lat, CultureInfo.InvariantCulture), Double.Parse(coordenadas.lon, CultureInfo.InvariantCulture), montevideoLat, montevideoLon);
            return distanciaAMontevideo <= radioMontevideo;

        }
     

        private double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Radio de la Tierra en kilómetros

            double lat1Rad = ConvertirAGradosARadianes(lat1);
            double lon1Rad = ConvertirAGradosARadianes(lon1);
            double lat2Rad = ConvertirAGradosARadianes(lat2);
            double lon2Rad = ConvertirAGradosARadianes(lon2);

            double dLat = lat2Rad - lat1Rad;
            double dLon = lon2Rad - lon1Rad;

            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Pow(Math.Sin(dLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = R * c;
            return distance;
        }

        public static double ConvertirAGradosARadianes(double grados)
        {
            return (Math.PI / 180) * grados;
        }
    }

}
