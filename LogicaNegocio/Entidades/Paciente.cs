﻿using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Paciente:Usuario, IValidar
    {
        //aca iria todo el tema de la agenda por ahora el paciente tiene solo los datos generales de Usuario

        public string Cedula { get; set; }
        public string Contacto { get; set; }

        public Paciente() { }
        public Paciente(string nombreUsr, string contrasenia, string nombre, string cedula,string contacto) : base(nombreUsr, contrasenia, nombre) { 
           
            Cedula = cedula;
            Contacto = contacto;
        }

        public void Validar()
        {
           //Implementar validacion
        }
    }
}
