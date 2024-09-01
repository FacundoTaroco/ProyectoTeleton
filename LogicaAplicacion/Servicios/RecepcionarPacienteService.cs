using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.Servicios
{
    //servicio que modifica el estado de las citas de un paciente en la base de datos central de Teleton
    public class RecepcionarPacienteService
    {
        private readonly IConfiguration _config;
        public RecepcionarPacienteService(IConfiguration config)
        {
            _config = config;
        }



        public async void RecepcionarPaciente(int pkAgenda) {

            try
            {
                var connectionString = _config["ConnectionStrings:SimuladorServidorCentral"];
                var commandText = "RecepcionarPaciente";
                // Establece la conexión
                List<CitaMedicaDTO> citasMedicas = new List<CitaMedicaDTO>();
                using (SqlConnection con = new(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@pkAgenda", pkAgenda));


                        con.Open();
                        await cmd.ExecuteNonQueryAsync();
                        con.Close();

                    }
                }
            }
            catch (TeletonServerException)
            {

                throw;
          
            }


        }

    }
}
