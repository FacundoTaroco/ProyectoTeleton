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
        private LibreriaContext _context;
        public RepositorioEncuesta(LibreriaContext context)
        {
            _context = context;
        }
        public void Add(Encuesta encuesta)
        {
            try
            {
                encuesta.Validar();
                encuesta.Id = 0;
                _context.Encuestas.Add(encuesta);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Encuesta> GetEncuestas()
        {
            try
            {
                IEnumerable<Encuesta> encuestas = new List<Encuesta>();
                encuestas = _context.Encuestas.ToList();
                return encuestas;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
