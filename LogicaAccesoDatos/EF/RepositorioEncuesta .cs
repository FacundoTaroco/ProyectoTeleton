using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioEncuesta : IRepositorioEncuesta
    {
        public List<Encuesta> _encuestas = new List<Encuesta>();
        public int _nextId = 1;

        public void Add(Encuesta entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            // Asignar un nuevo ID único (simulado, ajustar según la implementación real)
            entidad.Id = _nextId++;
            _encuestas.Add(entidad);
        }

        public void Guardar(Encuesta entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            // Buscar si la entidad ya existe en la lista
            var existente = _encuestas.FirstOrDefault(e => e.Id == entidad.Id);

            if (existente != null)
            {
                // Actualizar la entidad existente si se encuentra
                existente.SatisfaccionGeneral = entidad.SatisfaccionGeneral;
                existente.Comentarios = entidad.Comentarios;
            }
            else
            {
                // Si no existe, agregar como nueva entidad
                _encuestas.Add(entidad);
            }
        }

        public void Delete(int id)
        {
            var entidad = _encuestas.FirstOrDefault(e => e.Id == id);
            if (entidad != null)
                _encuestas.Remove(entidad);
            else
                throw new KeyNotFoundException($"Encuesta con ID {id} no encontrada.");
        }

        public void Update(Encuesta entidadActualizada)
        {
            if (entidadActualizada == null)
                throw new ArgumentNullException(nameof(entidadActualizada));

            // Buscar la entidad existente por su ID
            var existente = _encuestas.FirstOrDefault(e => e.Id == entidadActualizada.Id);

            if (existente == null)
                throw new KeyNotFoundException($"Encuesta con ID {entidadActualizada.Id} no encontrada.");

            // Actualizar las propiedades de la entidad existente con los valores de la entidad actualizada
            existente.SatisfaccionGeneral = entidadActualizada.SatisfaccionGeneral;
            existente.Comentarios = entidadActualizada.Comentarios;
        }

        public Encuesta GetPorId(int id)
        {
            return _encuestas.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Encuesta> GetAll()
        {
            return _encuestas;
        }


    }
}
