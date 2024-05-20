using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Excepciones
{
    public class NotFoundException : EFException
    {
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }
    }
}
