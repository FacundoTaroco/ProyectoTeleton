using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioEncuesta : IRepositorioEncuesta
    {
        private  LibreriaContext _context;

        public RepositorioEncuesta(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Encuesta entidad)
        {
            /*try
            {
                if (entidad == null)
                    throw new ArgumentNullException(nameof(entidad));

                _context.Encuestas.Add(entidad);
                _context.SaveChanges();
            }
            catch (Exception ex) {
                throw;
            }*/
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));
            if (string.IsNullOrWhiteSpace(entidad.Comentarios))
                throw new ArgumentException("Debe completar todos los campos", nameof(entidad.Comentarios));

            _context.Encuestas.Add(entidad);
            _context.SaveChanges();
        }

        public void Guardar(Encuesta entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            _context.Entry(entidad).State = entidad.Id == 0 ? EntityState.Added : EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entidad = _context.Encuestas.Find(id);
            if (entidad == null)
                throw new KeyNotFoundException($"Encuesta con ID {id} no encontrada.");

            _context.Encuestas.Remove(entidad);
            _context.SaveChanges();
        }

        public void Update(Encuesta entidadActualizada)
        {
            if (entidadActualizada == null)
                throw new ArgumentNullException(nameof(entidadActualizada));

            var existente = _context.Encuestas.Find(entidadActualizada.Id);
            if (existente == null)
                throw new KeyNotFoundException($"Encuesta con ID {entidadActualizada.Id} no encontrada.");

            existente.SatisfaccionGeneral = entidadActualizada.SatisfaccionGeneral;
            existente.Comentarios = entidadActualizada.Comentarios;

            _context.SaveChanges();
        }

        public Encuesta GetPorId(int id)
        {
            return _context.Encuestas.Find(id);
        }

        public IEnumerable<Encuesta> GetAll()
        {
            return _context.Encuestas.ToList();
        }
    }
}
