using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class PreguntaFrecException : DomainException
    {
        public PreguntaFrecException() { }
        public PreguntaFrecException(string message) : base(message) { }
    }
}
