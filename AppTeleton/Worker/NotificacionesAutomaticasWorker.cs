
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.CasosUso.PacienteCU;

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

                    try
                    {
                        _notificacionesAutomaticas.EnviarRecordatorioCitaMasTemprana();
                        Console.WriteLine("Notificacion automatica enviada con exito");
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Algo fallo al enviar notificaciones automaticas");
                    }

                    //delay en milisegundos entre que se ejecuta una tarea y otra
                    await Task.Delay(/*86400000*/30000, stoppingToken);
                }



            }
        }
    }
}
