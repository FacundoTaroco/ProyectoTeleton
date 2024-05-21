﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class CitaMedica
    {
        public int Id { get; set; } 
        public string TipoCita { get; set; }
        public string Materiales { get; set; }
        public string Personal { get; set; }
        public string Sala { get; set; }
        public DateTime Fecha { get; set; }
        public string CedulaPaciente { get; set; }


        public CitaMedica() { }

        public CitaMedica(string tipoCita, string materiales, string personal, string sala, DateTime fecha, string cedula)
        {
            TipoCita = tipoCita;
            Materiales = materiales;
            Personal = personal;
            Sala = sala;
            Fecha = fecha;
            CedulaPaciente = cedula;
        }   

    }
}
