using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class MensajeBotDTO
    {
        public string type { get; set; }
        public string message { get; set; }

        public MensajeBotDTO(string message)
        {
            this.type = "message";
            this.message = message;
        }
        public MensajeBotDTO() { }
    }
}
