using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class CoordenadasDTO
    {
        public string lat { get; set; }
        public string lon { get; set; }

        public CoordenadasDTO(string lat, string lon) {
                this.lat = lat;  
                this.lon= lon;
        
        }
        public CoordenadasDTO() { }
    }
}
