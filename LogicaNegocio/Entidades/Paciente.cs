using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Paciente:Usuario
    {
        //aca iria todo el tema de la agenda por ahora el paciente tiene solo los datos generales de Usuario

        public string Cedula { get; set; }

        public bool ParaEncuestar { get; set; } = false;

        public Paciente() { }
        public Paciente(string nombreUsr, string contrasenia, string nombre, string cedula) : base(nombreUsr, contrasenia, nombre) { 
           
            Cedula = cedula;
            ParaEncuestar = false;


        }

    }
}
