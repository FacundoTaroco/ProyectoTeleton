using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class PacienteBDDTO
    {
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }


        public PacienteBDDTO() { }
        public PacienteBDDTO(string documento, string nombreCompleto)
        {
            Documento = documento;
            NombreCompleto = nombreCompleto;
        }
    }
}
