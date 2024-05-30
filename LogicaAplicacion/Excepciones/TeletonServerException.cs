using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Excepciones
{
    public class TeletonServerException:Exception
    {
        public TeletonServerException() { }
        public TeletonServerException(string message) : base(message) { }
    }
}
