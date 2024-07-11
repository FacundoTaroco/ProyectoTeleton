using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesWit.Entrenamiento
{
    public class RespuestaEquivocada
    {
        public int Id { get; set; } 
        public string Input { get; set; }
        public string IntentAsignado { get; set; }

           
        //cuando implementemos entitis hacer esto
        // esto mas adelpublic Dictionary<string, object> EntidadesAsignadas { get; set; }

    }
}
