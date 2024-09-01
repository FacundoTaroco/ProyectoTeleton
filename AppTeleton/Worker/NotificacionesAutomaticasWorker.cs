
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.Entidades;

namespace AppTeleton.Worker
{
    public class NotificacionesAutomaticasWorker : BackgroundService
    {

        private readonly ILogger<CargarPacientesWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
        public NotificacionesAutomaticasWorker(ILogger<CargarPacientesWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }


        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //todos los dias a las 16:30 se tienen que enviar los recordatorios a todos los pacientes que tengan sus citas agendadas a un tiempo menor que el configurado
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
            DateTime primerLlamado = new DateTime(fechaHoy.Year, fechaHoy.Month, fechaHoy.Day, 16, 30, 0);

            if (fechaHoy > primerLlamado)
            {
                primerLlamado = primerLlamado.AddDays(1);
            }

            var delayInicial = primerLlamado - fechaHoy;
            _timer = new Timer(EnviarNotificaciones, null, delayInicial, TimeSpan.FromHours(24));

            await Task.CompletedTask;
        }

        private async void EnviarNotificaciones(object state)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                NotificacionesAutomaticas _notificacionesAutomaticas = scope.ServiceProvider.GetRequiredService<NotificacionesAutomaticas>();
                GetNotificacion _getNotificacion = scope.ServiceProvider.GetRequiredService<GetNotificacion>();
                ParametrosNotificaciones parametros = _getNotificacion.GetParametrosRecordatorios();

                try
                {
                    if (parametros.RecordatoriosEncendidos)
                    {
                        await _notificacionesAutomaticas.EnviarRecordatorioCitaMasTemprana();
                        Console.WriteLine("Notificacion automatica enviada con exito"); 
                        _logger.LogInformation("Notificación automática enviada con éxito");
                    }
                  
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Algo fallo al enviar notificaciones automaticas");
                    _logger.LogError(ex, "Algo falló al enviar notificaciones automáticas");
                }
            }
        }

        public override void Dispose()
        {
            // Asegura que el Timer se elimine correctamente al desechar el servicio
            _logger.LogError("Timer eliminado");
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
