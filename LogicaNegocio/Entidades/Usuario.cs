using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{

    public class Usuario : IValidar
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre del usuario")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el usuario de acceso")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Ingrese contraseña")]
        public string Contrasenia { get; set; }

        public Usuario(string nombreUsr, string contrasenia)
        {
            NombreUsuario = nombreUsr;
            Contrasenia = contrasenia;
        }
        public Usuario(string nombreUsr, string contrasenia, string nombre)
        {
            Nombre = nombre;
            NombreUsuario = nombreUsr;
            Contrasenia = contrasenia;
        }

        //constructor vacio
        public Usuario() { }

        public void Validar()
        {
            try
            {
                if (String.IsNullOrEmpty(Nombre) || String.IsNullOrEmpty(NombreUsuario) || String.IsNullOrEmpty(Contrasenia)) {
                    throw new UsuarioException("Ingrese todos los campos");
                }

                if(NombreUsuario.Length < 4)
                {
                    throw new UsuarioException("El nombre de usuario debe tener mas de 3 caracteres");
                }
                if(Contrasenia.Length < 4) { throw new UsuarioException("La contraseña debe tener mas de 3 caracteres"); }
            }
            catch (UsuarioException)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
