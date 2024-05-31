using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioRecepcionista:IRepositorioRecepcionista
    {
        private LibreriaContext _context;
        public RepositorioRecepcionista(LibreriaContext context)
        {
            _context = context;
        }
     
        public void Add(Recepcionista obj)
        {

            try
            {
                if (obj == null) { throw new NullOrEmptyException("No se recibio el usuario"); }
                obj.Validar();
                obj.Id = 0;
                ValidarUnique(obj);
                _context.Recepcionistas.Add(obj);
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
                throw new ServerErrorException("Error del servidor, algo fallo al agregar el usuario recepcionista");
            }
        }

        public void Delete(int id)
        {
            try
            {

                var recepcionista = GetPorId(id);
                _context.Recepcionistas.Remove(recepcionista);
                _context.SaveChanges();

            }
            catch (NullOrEmptyException) { throw; }
            catch (NotFoundException) { throw; }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al eliminar la recepcionista");
            }
        }
        public void ValidarUnique(Recepcionista obj)
        {
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
            catch (Exception)
            {
                throw;
            }

        }
        public IEnumerable<Recepcionista> GetAll()
        {
            try
            {
                IEnumerable<Recepcionista> recepcionistas = new List<Recepcionista>();
                recepcionistas = _context.Recepcionistas.ToList();
                return recepcionistas;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Fallo el servidor al obtener las recepcionistas");
            }
        }

        public Recepcionista GetRecepcionistaPorUsuario(string usuario)
        {
            try
            {
                if (String.IsNullOrEmpty(usuario))
                {
                    throw new NullOrEmptyException("No se recibio cedula");
                }
                var recepcionista = _context.Recepcionistas.FirstOrDefault(rec => rec.NombreUsuario.Equals(usuario));
                if (recepcionista == null)
                {
                    throw new NotFoundException("La cedula ingresada no existe");
                }
                return recepcionista;

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

        public Recepcionista GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new NullOrEmptyException("No se recibio id");
                }
                var recepcionista = _context.Recepcionistas.FirstOrDefault(recep => recep.Id == id);
                if (recepcionista == null)
                {
                    throw new NotFoundException("No se encontro ninguna recepcionista con el id ingresado");
                }
                return recepcionista;

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

        public void Update(Recepcionista obj)
        {
            try
            {
                if (obj == null) { throw new Exception("No se recibio recepcionista para editar"); }
                obj.Validar();

                if (_context.Recepcionistas.FirstOrDefault(r => r.Id == obj.Id) == null)
                {
                    throw new NotFoundException("No se encontro recepcionista a editar");
                }
                if (_context.Recepcionistas.FirstOrDefault(r => r.Id == obj.Id).NombreUsuario != obj.NombreUsuario)
                {
                    throw new UsuarioException("El nombre de usuario no se puede editar");
                }

                _context.Recepcionistas.Update(obj);
                _context.SaveChanges();
            }
            catch (NotFoundException)
            {
                throw;
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
