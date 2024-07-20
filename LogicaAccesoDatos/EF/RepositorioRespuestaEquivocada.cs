using LogicaAccesoDatos.EF.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioRespuestaEquivocada : IRepositorioRespuestasEquivocadas
    {

        private LibreriaContext _context;
        public RepositorioRespuestaEquivocada(LibreriaContext context)
        {
            _context = context;
        }
        public void Add(RespuestaEquivocada obj)
        {
            try
            {


                if (obj == null) { throw new NullOrEmptyException("No se recibio respuesta equivocada"); }
                obj.Id = 0;
                _context.RespuestasEquivocadas.Add(obj);
                _context.SaveChanges();


            }
            catch (NullOrEmptyException)
            {
                throw;
            }
            catch (DispositivoNotificacionException)
            {
                throw;
            }
            catch (UniqueException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServerErrorException("Error del servidor, algo fallo al agregar la respuesta");
            }
        }

        public void Delete(int id)
        {
            try
            {
                RespuestaEquivocada res = _context.RespuestasEquivocadas.FirstOrDefault(d => d.Id == id);
                _context.RespuestasEquivocadas.Remove(res);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<RespuestaEquivocada> GetAll()
        {
            try
            {
                IEnumerable<RespuestaEquivocada> respuestas = new List<RespuestaEquivocada>();
                respuestas = _context.RespuestasEquivocadas.ToList();
                return respuestas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RespuestaEquivocada GetPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(RespuestaEquivocada obj)
        {
            throw new NotImplementedException();
        }
    }
}
