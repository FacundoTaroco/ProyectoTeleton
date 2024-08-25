using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enums;
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

        public void CambiarContrasenia(Usuario usu) {

            try
            {
                if (usu == null) {
                    throw new Exception("No se recibio usuario para cambiar la contraseña");
                }
                usu.Validar();

                _context.Usuarios.Update(usu);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        
        }

        public Usuario GetUsuarioPorNombre(string nombreUsu)
        {
            try
            {
                if (String.IsNullOrEmpty(nombreUsu)) { throw new NullOrEmptyException(); }
                Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsu);
                if (usuario == null) { throw new NotFoundException(); }
                return usuario;
            }
            catch (NullOrEmptyException)
            {

                throw;
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario GetUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario == 0) { throw new NullOrEmptyException(); }
                Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
                if (usuario == null) { throw new NotFoundException(); }
                return usuario; 
            }
            catch (NullOrEmptyException)
            {

                throw;
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TipoUsuario Login(string usuario, string contrasenia)
        {
            try
            {
                TipoUsuario tipoUsuario = TipoUsuario.NoLogueado;
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
                                tipoUsuario = TipoUsuario.Totem;
                            } else if (u is Paciente) {
                                tipoUsuario = TipoUsuario.Paciente;
                            } else if (u is Recepcionista) {
                                tipoUsuario = TipoUsuario.Recepcionista;
                            } else if (u is Administrador) {
                                tipoUsuario = TipoUsuario.Admin;
                            } else if(u is Medico)
                            {
                                tipoUsuario = TipoUsuario.Medico;

                            }
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
                    throw new NotFoundException("Usuario Incorrecto");
                }
                return tipoUsuario;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (UsuarioException)
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
