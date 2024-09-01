using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.Servicios
{
    //Servicio para obtener las citas de uno o varios pacientes desde la base de datos central de teleton
    public class SolicitarCitasService
    {
 
        private readonly IConfiguration _config;
        public SolicitarCitasService(IConfiguration config)
        {
            _config = config;
           
        }
        public async Task<IEnumerable<CitaMedicaDTO>> ObtenerCitas() {


                var connectionString = _config["ConnectionStrings:SimuladorServidorCentral"];
                var commandText = "SELECT * FROM GetAgendas()";
                SqlConnection con = new(connectionString);
            try
            {
                
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
                            string cedula = reader.GetString(1);
                            string nombre = reader.GetString(2);
                            string servicio = reader.GetString(3);
                            DateTime fecha = reader.GetDateTime(4).AddDays(1);                         
                            int horaInicio = reader.GetInt32(5);
                            string tratamiento = reader.GetString(6);
                            string consultorio = reader.GetString(7);
                            string estado = reader.GetString(8);
                            CitaMedicaDTO cita = new CitaMedicaDTO(pkAgenda,cedula,nombre,servicio,fecha,horaInicio,tratamiento,consultorio,estado);
                            citasMedicas.Add(cita);
                        }
                        con.Close();
                    }
                }
                return citasMedicas;
            }
            catch (Exception)
            {
                con.Close();
                throw new TeletonServerException("Error de conexion con el servidor central");
            }
           
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
                            DateTime fecha = reader.GetDateTime(4).AddDays(1); 
                            int horaInicio = reader.GetInt32(5);
                            string tratamiento = reader.GetString(6);
                            string consultorio = reader.GetString(7);
                            string estado = reader.GetString(8);
                            CitaMedicaDTO cita = new CitaMedicaDTO(pkAgenda, ci, nombre, servicio, fecha, horaInicio, tratamiento, consultorio,estado);
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
           

        }

    }

}



