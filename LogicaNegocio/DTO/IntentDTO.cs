using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DTO
{
    public class IntentDTO
    {
        public string name { get; set; }
        public IntentDTO(string name)
        {

            this.name = name;
        }
        public IntentDTO() { }
    }
}
