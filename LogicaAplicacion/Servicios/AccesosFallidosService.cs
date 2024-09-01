using LogicaAplicacion.CasosUso.ChatCU;
using LogicaAplicacion.CasosUso.CitaCU;
using LogicaAplicacion.CasosUso.TotemCU;
using LogicaAplicacion.Excepciones;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Servicios
{
    //Servicio que reintenta la comunicacion con el servidor central en caso de que este se encontrara caido a la hora de que un paciente acceda al totem
    public class AccesosFallidosService
    {
        private static Timer _timer;
        private static readonly object _lock = new object();
        private static bool _isRunning = false;

        public static void IniciarServicioDeReintento(GetCitas _getCitas, GenerarAvisoLlegada _generarAvisoLlegada)
        {
            // Inicia el Timer para ejecutar ColaDeAccesosFallidos cada 20 minutos
            if (_timer == null)
            {
                //.FromMinutes(20);
                _timer = new Timer(state => ColaDeAccesosFallidos(state, _getCitas, _generarAvisoLlegada),
                           null, TimeSpan.Zero, TimeSpan.FromMinutes(20));
            }
        }

        private static void ColaDeAccesosFallidos(object state, GetCitas _getCitas, GenerarAvisoLlegada _generarAvisoLlegada)
        {
            lock (_lock)
            {
                
                if (_isRunning) return; // Evita que se ejecute en paralelo si ya está en proceso
                _isRunning = true;
            }

            try
            {
                // Ejecuta la lógica de reintento
                ProcesarAccesosFallidos(_getCitas, _generarAvisoLlegada).Wait();
            }
            finally
            {
                _isRunning = false;
            }
        }

        private static async Task ProcesarAccesosFallidos(GetCitas _getCitas, GenerarAvisoLlegada _generarAvisoLlegada)
        {
            List<AccesoTotem> accesos;

            lock (_lock)
            {
                accesos = AccesosFallidos.accesosFallidos.ToList();
            }

            foreach (AccesoTotem a in accesos)
            {
                bool accesoEnviado = await GenerarAvisoLLegada(a, _getCitas, _generarAvisoLlegada);

                if (accesoEnviado)
                {
                    lock (_lock)
                    {
                        AccesosFallidos.accesosFallidos.Remove(a);
                    }
                }
            }

            if (AccesosFallidos.accesosFallidos.Count == 0)
            {
                AccesosFallidos.servicioDeReintentoActivado = false;
                _timer?.Dispose(); // detiene el timer ya que no hay mas accesos fallidos para procesar
                _timer = null;
            }
        }

        private static async Task<bool> GenerarAvisoLLegada(AccesoTotem acceso, GetCitas _getCitas, GenerarAvisoLlegada _generarAvisoLlegada)
        {
            try
            {
                IEnumerable<CitaMedicaDTO> citas = await _getCitas.ObtenerCitasPorCedula(acceso.CedulaPaciente);
                IEnumerable<CitaMedicaDTO> citasDeHoy = citas.Where(c => c.Cedula == acceso.CedulaPaciente && (c.Fecha.Day == acceso.FechaHora.Day && c.Fecha.Month == acceso.FechaHora.Month && c.Fecha.Year == acceso.FechaHora.Year)).OrderBy(c => c.HoraInicio).ToList();

                foreach (var cita in citasDeHoy)
                {
                    _generarAvisoLlegada.GenerarAvisoLLamada(cita.PkAgenda);
                    cita.Estado = "RCP";
                }

                return true; // Retorna true si se pudo conectar con  el servidor
            }
            catch (TeletonServerException)
            {
                return false; // Retorna false si hubo un error al contactar el servidor central
            }
        }
    }
}
