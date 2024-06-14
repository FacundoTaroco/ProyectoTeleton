using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class ConexionChat
    {
        public string idConexion { get; set; }
        public string nombreUsuario { get; set; }

        public ConexionChat() { }
        public ConexionChat(string idConexion, string nombreUsuario)
        {
            this.idConexion = idConexion;
            this.nombreUsuario = nombreUsuario;
        }
    }
}
