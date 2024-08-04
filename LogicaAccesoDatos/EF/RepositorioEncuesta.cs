using LogicaNegocio.DTO;
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

        public double GetPromedioSatisfaccionGeneral() {
            try
            {
                IEnumerable<Encuesta> encuestas = _context.Encuestas.ToList();

                double agregado = 0;
                int contador = 0;
                if (encuestas.Count() > 0)
                {
                    foreach (var encuesta in encuestas) {

                        if (encuesta.SatisfaccionGeneral != 0) {

                            agregado += encuesta.SatisfaccionGeneral;
                            contador++;

                        }
                    }

                    return agregado/contador;   
                }
                else {

                    return 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetPromedioSatisfaccionRecepcion()
        {
            try
            {
                IEnumerable<Encuesta> encuestas = _context.Encuestas.ToList();
                double agregado = 0;
                int contador = 0;
                if (encuestas.Count() > 0)
                {
                    foreach (var encuesta in encuestas)
                    {

                        if (encuesta.SatisfaccionRecepcion != 0)
                        {

                            agregado += encuesta.SatisfaccionRecepcion;
                            contador++;

                        }
                    }

                    return agregado / contador;
                }
                else
                {

                    return 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public double GetPromedioSatisfaccionEstadoCentro()
        {
            try
            {
                IEnumerable<Encuesta> encuestas = _context.Encuestas.ToList();
                double agregado = 0;
                int contador = 0;
                if (encuestas.Count() > 0)
                {
                    foreach (var encuesta in encuestas)
                    {

                        if (encuesta.SatisfaccionEstadoDelCentro != 0)
                        {

                            agregado += encuesta.SatisfaccionEstadoDelCentro;
                            contador++;

                        }
                    }

                    return agregado / contador;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetPromedioSatisfaccionAplicacion()
        {
            try
            {
                IEnumerable<Encuesta> encuestas = _context.Encuestas.ToList();
                double agregado = 0;
                int contador = 0;
                if (encuestas.Count() > 0)
                {
                    foreach (var encuesta in encuestas)
                    {

                        if (encuesta.SatisfaccionAplicacion != 0)
                        {

                            agregado += encuesta.SatisfaccionAplicacion;
                            contador++;

                        }
                    }

                    return agregado / contador;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ComentarioEncuestaDTO> GetComentariosPuntuados() {
            try
            {
                IEnumerable<Encuesta> encuestas = _context.Encuestas.ToList();
                List<ComentarioEncuestaDTO> comentarios = new List<ComentarioEncuestaDTO>();
                if (encuestas.Count() > 0)
                {
                    foreach (var encuesta in encuestas)
                    {
                        ComentarioEncuestaDTO comentario = new ComentarioEncuestaDTO(); 
                        comentario.Comentario = encuesta.Comentarios;
                        comentario.SatisfaccionGeneral = encuesta.SatisfaccionGeneral;
                        comentario.Fecha = encuesta.Fecha;
                        comentarios.Add(comentario);
                    }

                    return comentarios;
                }
                else
                {
                 return new List<ComentarioEncuestaDTO>();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
