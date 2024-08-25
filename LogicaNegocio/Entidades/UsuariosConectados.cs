

namespace LogicaNegocio.Entidades
{
    public class UsuariosConectados
    {

        public static List<ConexionChat> usuariosConectados = new List<ConexionChat>();

        public static string GetIdConexionDeUsuario(string nombreUsuario) {
            foreach (ConexionChat c in usuariosConectados) {

                if (c.nombreUsuario.Equals(nombreUsuario)) { 
                    return c.idConexion;
                
                }
            }

            return "";
        }

        public static void BorrarConexionPorId(string idConexion) {

            for (int i = 0; i<usuariosConectados.Count();i++) {

                if (usuariosConectados[i].idConexion.Equals(idConexion)) { 
                
                    usuariosConectados.RemoveAt(i);
                
                }
            
            
            }
        
        
        }
    }
}
