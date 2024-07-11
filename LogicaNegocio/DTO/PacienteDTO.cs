using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class PacienteDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Contraseña { get; set; }

        public PacienteDTO() { }
        public PacienteDTO(int id, string nombreCompleto, string cedula, string contraseña)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            Cedula = cedula;
            Contraseña = contraseña;
        }
    }
}
