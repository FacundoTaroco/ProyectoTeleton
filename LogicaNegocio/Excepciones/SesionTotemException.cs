using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class SesionTotemException:DomainException
    {
        public SesionTotemException() { }
        public SesionTotemException(string message) : base(message) { }
    }
}
