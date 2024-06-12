﻿using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        /*public string linkAPI { get; set; }*/
        private readonly IConfiguration _config;
        public SolicitarCitasService(IConfiguration config)
        {
            _config = config;
            /*linkAPI = "https://localhost:7201/";*/
        }
        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitas() {

            try
            {
                var connectionString = _config["ConnectionStrings:TeletonSimuladorDatabase"];
                var commandText = "SELECT * FROM GetAgendas()";
                // Establece la conexión
                List<CitaMedicaDTO> citasMedicas = new List<CitaMedicaDTO>();
                using (SqlConnection con = new(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {
                        con.Open();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            int pkAgenda = reader.GetInt32(0);
                            string cedula = reader.GetString(1);
                            string nombre = reader.GetString(2);
                            string servicio = reader.GetString(3);
                            DateTime fecha = reader.GetDateTime(4);
                            int horaInicio = reader.GetInt32(5);
                            string tratamiento = reader.GetString(6);
                            CitaMedicaDTO cita = new CitaMedicaDTO(pkAgenda,cedula,nombre,servicio,fecha,horaInicio,tratamiento);
                            citasMedicas.Add(cita);
                        }
                        con.Close();
                    }
                }
                return citasMedicas;
            }
            catch (Exception)
            {

                throw new TeletonServerException("Error de conexion con el servidor central");
            }
            /* try
             {
                 var options = new RestClientOptions(linkAPI)
                 {
                     MaxTimeout = -1,
                 };
                 var client = new RestClient(options);
                 var request = new RestRequest("api/Cita", Method.Get);
                 /*request.AddHeader("Authorization", $"Bearer {token}");
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
             }*/
        }
        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitasPorCedula(string cedula)
        {
            
            
            var connectionString = _config["ConnectionStrings:SimuladorServidorCentral"];
            var commandText = $"SELECT * FROM GetAgendasDePaciente({cedula})";
            SqlConnection con = new(connectionString);
            try
            {
                // Establece la conexión
                List<CitaMedicaDTO> citasMedicas = new List<CitaMedicaDTO>();
                using (con)
                {
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {
                        con.Open();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            int pkAgenda = reader.GetInt32(0);
                            string ci = reader.GetString(1);
                            string nombre = reader.GetString(2);
                            string servicio = reader.GetString(3);
                            DateTime fecha = reader.GetDateTime(4);
                            int horaInicio = reader.GetInt32(5);
                            string tratamiento = reader.GetString(6);
                            CitaMedicaDTO cita = new CitaMedicaDTO(pkAgenda, ci, nombre, servicio, fecha, horaInicio, tratamiento);
                            citasMedicas.Add(cita);
                        }
                        reader.Close();
                        con.Close();
                    }
                }
                return citasMedicas;
            }


            catch (Exception e)
            {
                con.Close();
                throw new TeletonServerException("Error de conexion con el servidor central, " + e.Message);
            }
            /*try
            {
                var options = new RestClientOptions(linkAPI)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("api/Cita/GetPorCedula/"+cedula, Method.Get);
                /*request.AddHeader("Authorization", $"Bearer {token}");
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
            }*/

        }

    }

}



