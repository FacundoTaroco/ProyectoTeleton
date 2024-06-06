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

        public PreguntaFrec() { }

        public PreguntaFrec(string pregunta)
        {
            Pregunta = pregunta;
        }

        public void Validar()
        {
            try
            {
                if (String.IsNullOrEmpty(Pregunta))
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
