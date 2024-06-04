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
    public class RepositorioAdministrador:IRepositorioAdministrador
    {
        private LibreriaContext _context;
        public RepositorioAdministrador(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Administrador obj)
        {
            try
            {
                if (obj == null) { throw new NullOrEmptyException("No se recibio el admin"); }//hacer algunas excepciones personalizadas 
                obj.Validar();
                obj.Id = 0;
              
                ValidarUnique(obj);
                _context.Administradores.Add(obj);
                _context.SaveChanges();
            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (UsuarioException)
            {
                throw;
            }
            catch (UniqueException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor, algo fallo al agregar el usuario admin");
            }
        }
        public void ValidarUnique(Administrador obj) {
            try
            {
                foreach (Usuario a in _context.Usuarios.ToList())
                {

                    if (a.NombreUsuario.Equals(obj.NombreUsuario))
                    {
                        throw new UniqueException("El usuario ya existe, ingrese otro nombre de usuario");
                    }
                }
            }
            catch (UniqueException)
            {

                throw;
            }
            catch(Exception)
            {
                throw;
            }
       
        }

        public void Delete(int id)
        {
            try
            {

                var admin = GetPorId(id);
              
                _context.Administradores.Remove(admin);

                _context.SaveChanges();

            }
            catch (Exception) { throw; }
        }

        public IEnumerable<Administrador> GetAll()
        {
            try
            {
                IEnumerable<Administrador> admins = new List<Administrador>();
                admins = _context.Administradores.ToList();
                return admins;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al obtener los administradores");
            }
        }



        public Administrador GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new NullOrEmptyException("No se recibio id");
                }
                var admin = _context.Administradores.FirstOrDefault(adm => adm.Id == id);
                if (admin == null)
                {
                    throw new NotFoundException("No se encontro ningun admin con esa cedula");
                }
                return admin;

            }
            catch(NullOrEmptyException) 
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

        public void Update(Administrador obj)
        {
            try
            {
                if (obj == null) { throw new NullOrEmptyException("No se recibio administrador para editar"); }
                obj.Validar();
                if (_context.Administradores.FirstOrDefault(r => r.Id == obj.Id) == null)
                {
                    throw new NotFoundException("No se encontro recepcionista a editar");
                }
                if (_context.Administradores.FirstOrDefault(r => r.Id == obj.Id).NombreUsuario != obj.NombreUsuario)
                {
                    throw new UsuarioException("El nombre de usuario no se puede editar");
                }

                _context.Administradores.Update(obj);
                _context.SaveChanges();
            }
            catch (UsuarioException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al actualizar el administrador");
            }
        }


    }
}
