﻿using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class PreguntaFrec:IValidar
    {
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public CategoriaPregunta CategoriaPregunta { get; set; }

        public PreguntaFrec() { }

        public PreguntaFrec(string pregunta, CategoriaPregunta categoria)
        {
            Pregunta = pregunta;
            CategoriaPregunta = categoria;
       
        }

        public void Validar()
        {
            try
            {
                if (String.IsNullOrEmpty(Pregunta) || CategoriaPregunta == null)
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
