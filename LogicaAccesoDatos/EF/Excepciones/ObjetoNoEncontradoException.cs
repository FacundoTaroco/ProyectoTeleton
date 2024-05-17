using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Excepciones
{
    public class ObjetoNoEncontradoException:Exception
    {
        public ObjetoNoEncontradoException() { }
        public ObjetoNoEncontradoException(string message) : base(message) { }
    }
}
