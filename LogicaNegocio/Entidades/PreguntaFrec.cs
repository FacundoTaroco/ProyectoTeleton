using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class PreguntaFrec
    {
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }

        public PreguntaFrec() { }

        public PreguntaFrec(string pregunta, string respuesta)
        {
            Pregunta = pregunta;
            Respuesta = respuesta;
        }

        public void Validar()
        {
            try
            {
                if (String.IsNullOrEmpty(Pregunta) || String.IsNullOrEmpty(Respuesta))
                {
                    throw new PreguntaFrecException("Ingrese todos los campos");
                }
            }
            catch (PreguntaFrecException)
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
