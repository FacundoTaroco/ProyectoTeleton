using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Excepciones
{
    public class EFException : Exception
    {
        public EFException() { }

        public EFException(string message) : base(message) { }
    }
}
