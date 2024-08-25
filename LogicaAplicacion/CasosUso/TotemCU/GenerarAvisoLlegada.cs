using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.TotemCU
{
    public class GenerarAvisoLlegada
    {

        RecepcionarPacienteService _servicioAvisoMedico;
        public GenerarAvisoLlegada(RecepcionarPacienteService servicioAviso) {

            _servicioAvisoMedico = servicioAviso;
        }

        public async void GenerarAvisoLLamada(int pkAgenda) {
            try
            {
               _servicioAvisoMedico.RecepcionarPaciente(pkAgenda);
            }
            catch (Exception)
            {
                //ACA VER QUE HACER SI NO SE PUEDE GENERAR EL AVISO REQUERIMIENTO RF13 (Recepción Automatizada de Usuarios)
                //no podemos tirar excepciones porque nos tranca la accion del controller de totem por lo que los diferentes avisos se tienen que guardar
                //en una pila de llamados que se envian al servidor una vez que este se encuentra disponible con cierta politica de reintento
                throw;
            }
            
        
        
        }
    }
}
