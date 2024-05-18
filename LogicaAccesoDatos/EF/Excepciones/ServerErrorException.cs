using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Excepciones
{
    public class ServerErrorException:EFException
    {
        public ServerErrorException() { }

        public ServerErrorException(string message) : base(message) { }
    }
}
