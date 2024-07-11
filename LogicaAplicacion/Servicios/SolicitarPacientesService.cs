﻿using LogicaNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Text.Json;
using System.Net;
using LogicaAplicacion.Excepciones;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LogicaAplicacion.Servicios


{
    //esta clase es la encargada de solicitar el listado de pacientes del servidor de teleton para luego enviarselo a ActualizarPacientes
    public class SolicitarPacientesService
    {
       /* public string linkAPI { get; set; }*/
       private readonly IConfiguration _config;
        public SolicitarPacientesService(IConfiguration config) {


            /*linkAPI = "https://localhost:7201/";*/

            _config=config;


        }

        public async Task<IEnumerable<PacienteDTO>> solicitarPacientesATeleton() {

            var connectionString = _config["ConnectionStrings:SimuladorServidorCentral"];
            var commandText = "SELECT * FROM GetPacientes()";
            SqlConnection con = new(connectionString);
            try
            {
                // Establece la conexión
                List<PacienteDTO> pacientesBaseDeDatos = new List<PacienteDTO>();
                using (con)
                {

                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {

                        con.Open();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        while (reader.Read())
                        {

                            string Documento = reader.GetString(0);
                            string Nombre = reader.GetString(1);
                            string Contrasenia= reader.GetString(2);
                            int Id = reader.GetInt32(3);

                            PacienteDTO paciente = new PacienteDTO(Id, Nombre, Documento, Contrasenia);
                            pacientesBaseDeDatos.Add(paciente);


                        }
                        reader.Close();
                        con.Close();
                    }
                }


                return pacientesBaseDeDatos;
            }
            catch (Exception e)
            {
                con.Close();
                throw new TeletonServerException("Error de conexion con el servidor central, "+ e.Message);
            }

           

        }


    }

}
