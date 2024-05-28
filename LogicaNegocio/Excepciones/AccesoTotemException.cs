using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class AccesoTotemException:DomainException
    {
        public AccesoTotemException() { }
        public AccesoTotemException(string message) : base(message) { }
    }
}
