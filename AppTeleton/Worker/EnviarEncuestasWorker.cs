
using LogicaAplicacion.CasosUso.EncuestaCU;
using LogicaAplicacion.CasosUso.PacienteCU;

namespace AppTeleton.Worker
{
    public class EnviarEncuestasWorker : BackgroundService
    {


        private readonly ILogger<EnviarEncuestasWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
     

        public EnviarEncuestasWorker(IServiceProvider serviceProvider, ILogger<EnviarEncuestasWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //todos los dias a las 5pm se tiene que verificar y enviar encuestas a todos los pacientes que hayan accedido al totem ese dia
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
            DateTime primerLlamado = new DateTime(fechaHoy.Year, fechaHoy.Month, fechaHoy.Day, 20, 01, 0);

            if (fechaHoy > primerLlamado)
            {
                primerLlamado = primerLlamado.AddDays(1);
            }

            var delayInicial = primerLlamado - fechaHoy;
            _timer = new Timer(EnviarEncuestas, null, delayInicial, TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        private void EnviarEncuestas(object state) {

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                EnviarEncuestas _enviarEncuestas = scope.ServiceProvider.GetRequiredService<EnviarEncuestas>();

                try
                {
                    _enviarEncuestas.EnviarSolicitudEncuestas();
                    Console.WriteLine("Encuestas enviadas con exito");
                }
                catch (Exception)
                {

                    Console.WriteLine("Fallo al enviar las encuestas");
                }
            }
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
