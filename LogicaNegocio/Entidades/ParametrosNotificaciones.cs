using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class ParametrosNotificaciones : IValidar
    {
        public static ParametrosNotificaciones instancia;
        public int Id { get; set; }

        public int CadaCuantoEnviarRecordatorio { get; set; }

        public bool RecordatoriosEncendidos { get; set; }

    

        public ParametrosNotificaciones()
        {
            CadaCuantoEnviarRecordatorio = 2;
            RecordatoriosEncendidos = true;
        }

        public static ParametrosNotificaciones GetInstancia() {

            if (instancia == null) { 
            
                instancia = new ParametrosNotificaciones();
            }
            return instancia;
        }

        public void Validar()
        {
            if (CadaCuantoEnviarRecordatorio < 1) {
           
                throw new Exception("Los recordatorios pueden enviarse como mínimo con un dia de antelación, ingrese un numero mayor a 0");
            }
        }
    }
}
