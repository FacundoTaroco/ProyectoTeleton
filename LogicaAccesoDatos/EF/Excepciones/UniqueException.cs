using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF.Excepciones
{
    public class UniqueException:EFException
    {
        public UniqueException() { }

        public UniqueException(string message) : base(message) { }
    }
}
