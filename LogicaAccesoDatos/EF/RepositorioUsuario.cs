using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioUsuario : IRepositorioUsuario
    {

        private LibreriaContext _context;
        public RepositorioUsuario(LibreriaContext context)
        {
            _context = context;
        }
        public string Login(string usuario, string contrasenia)
        {
            try
            {
                string tipoUsuario = "";
                bool Vmail = false;
                bool Vcontra = false;   
                IEnumerable<Usuario> usuarios = _context.Usuarios.ToList();
                foreach (Usuario u in usuarios)
                {
                    if (u.NombreUsuario == usuario)
                    {
                        Vmail = true;
                        if (u.Contrasenia == contrasenia)
                        {
                            if (u is Totem) {
                                tipoUsuario = "TOTEM";
                              
                            }//if u is Paciente... 
                        Vcontra = true;
                        }

                    }
                }
                if (Vcontra == false && Vmail)
                {
                    throw new UsuarioException("Contraseña Incorrecta");
                }
                else if (Vmail == false)
                {
                    throw new NotFoundException("Mail o usuario Incorrecto");
                }
                return tipoUsuario;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (DomainException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ServerErrorException("No se pudo loguear, " + e.Message);
            }



        }


    }
}
