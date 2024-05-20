using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Excepciones
{
    public class ApiErrorException:Exception
    {

        public ApiErrorException() { }
        public ApiErrorException(string message) : base(message) { }
    }
}
