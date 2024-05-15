using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    //esta clase es para obtener errores de comunicacion con APIs
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
