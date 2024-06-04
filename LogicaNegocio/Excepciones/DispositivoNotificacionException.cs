using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class DispositivoNotificacionException:DomainException
    {
        public DispositivoNotificacionException() { }

        public DispositivoNotificacionException(string message) : base(message) { }
    }
}
