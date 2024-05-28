using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Excepciones
{
    public class NullOrEmptyException:EFException
    {
        public NullOrEmptyException() { }
        public NullOrEmptyException(string message) : base(message) { }
    }
}
