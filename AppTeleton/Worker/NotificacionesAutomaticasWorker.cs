﻿
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;

namespace AppTeleton.Worker
{
    public class NotificacionesAutomaticasWorker : BackgroundService
    {

        private readonly ILogger<CargarPacientesWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public NotificacionesAutomaticasWorker(ILogger<CargarPacientesWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }


        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    NotificacionesAutomaticas _notificacionesAutomaticas = scope.ServiceProvider.GetRequiredService<NotificacionesAutomaticas>();
                    GetNotificacion _getNotificacion = scope.ServiceProvider.GetRequiredService<GetNotificacion>();
                    ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();

                    try
                    {
                        if (parametros.RecordatoriosEncendidos) { 
                            await _notificacionesAutomaticas.EnviarRecordatorioCitaMasTemprana();
                            Console.WriteLine("Notificacion automatica enviada con exito");
                        }
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Algo fallo al enviar notificaciones automaticas");
                    }

                   
                    //ACA SIEMPRE ES UNA VEZ POR DIA
                    //delay en milisegundos entre que se ejecuta una tarea y otra
                    await Task.Delay(10000, stoppingToken);
                }



            }
        }
    }
}
