using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class CategoriaPregunta
    {
        public int Id { get; set; } 
        public string Categoria { get; set; }
        public string Respuesta { get; set; }

        public CategoriaPregunta() { 
        
        
        }

        public CategoriaPregunta(string categoria, string respuesta)
        {
            Categoria = categoria;
            Respuesta = respuesta;
        }
    }
}
