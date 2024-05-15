using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class PacienteDTO
    {
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Contacto { get; set; }
        public PacienteDTO() { }
        public PacienteDTO(string nombreCompleto, string cedula, string contacto)
        {
            NombreCompleto = nombreCompleto;
            Cedula = cedula;
            Contacto = contacto;
        }
    }
}
