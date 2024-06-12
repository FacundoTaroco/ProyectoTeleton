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
    public class RepositorioPreguntaFrec : IRepositorioPreguntaFrec
    {
        private LibreriaContext _context;
        public RepositorioPreguntaFrec(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(PreguntaFrec obj)
        {

            try
            {
                if (obj == null) { throw new NullOrEmptyException("No se recibio la pregunta"); }
                obj.Validar();
                obj.Id = 0;
                ValidarUnique(obj);
                _context.PreguntasFrec.Add(obj);
                _context.SaveChanges();

            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (PreguntaFrecException)
            {
                throw;
            }
            catch (UniqueException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor, algo fallo al agregar la pregunta frecuente");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var preguntaFrec = GetPorId(id);
                _context.PreguntasFrec.Remove(preguntaFrec);
                _context.SaveChanges();

            }
            catch (NullOrEmptyException) { throw; }
            catch (NotFoundException) { throw; }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor al eliminar la pregunta frecuente");
            }
        }

        public void Update(PreguntaFrec obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new Exception("No se recibió pregunta frecuente para editar");
                }

                obj.Validar();

                var existingPregunta = _context.PreguntasFrec.FirstOrDefault(r => r.Id == obj.Id);

                if (existingPregunta == null)
                {
                    throw new NotFoundException("No se encontró pregunta frecuente a editar");
                }
                _context.Entry(existingPregunta).CurrentValues.SetValues(obj);
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

        public void ValidarUnique(PreguntaFrec obj)
        {
            try
            {
                foreach (PreguntaFrec a in _context.PreguntasFrec.ToList())
                {

                    if (a.Pregunta.Equals(obj.Pregunta))
                    {
                        throw new UniqueException("La pregunta frecuente ya existe, ingrese otra pegunta");
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

        public IEnumerable<PreguntaFrec> GetAll()
        {
            try
            {
                IEnumerable<PreguntaFrec> PreguntasFrec = new List<PreguntaFrec>();
                PreguntasFrec = _context.PreguntasFrec.ToList();
                return PreguntasFrec;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Fallo el servidor al obtener las preguntas frecuentes");
            }
        }

        public PreguntaFrec GetPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new NullOrEmptyException("No se recibio id");
                }
                var preguntaFrec = _context.PreguntasFrec.FirstOrDefault(recep => recep.Id == id);
                if (preguntaFrec == null)
                {
                    throw new NotFoundException("No se encontro ninguna pregunta frecunte con el id ingresado");
                }
                return preguntaFrec;

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
    }
}
