using LogicaAplicacion.CasosUso.PacienteCU;

namespace AppTeleton.Worker
{
    public class CargarPacientesWorker:BackgroundService
    {
        //Este es un programa que queda corriendo en segundo plano y permite actualizar los pacientes una vez cada el tiempo que queramos

        private readonly ILogger<CargarPacientesWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
         private Timer _timer;
        public CargarPacientesWorker(ILogger<CargarPacientesWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //todos los dias a las 11pm se tiene que verificar si se ingresaron pacientes nuevos al servidor central y cargarlos en la aplicacion
            DateTime _fecha = DateTime.UtcNow;
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(_fecha, zonaHoraria);
            DateTime primerLlamado = new DateTime(fechaHoy.Year, fechaHoy.Month, fechaHoy.Day, 23, 00, 0);

            if (fechaHoy > primerLlamado)
            {
                primerLlamado = primerLlamado.AddDays(1);
            }

            var delayInicial = primerLlamado - fechaHoy;
            _timer = new Timer(CargarPacientes, null, delayInicial, TimeSpan.FromHours(24));

        }

        private async void CargarPacientes(object state) {

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ActualizarPacientes _actualizarPacientes = scope.ServiceProvider.GetRequiredService<ActualizarPacientes>();

                try
                {
                    await _actualizarPacientes.Actualizar();
                    Console.WriteLine("Pacientes Actualizados con exito");
                }
                catch (Exception)
                {

                    Console.WriteLine("Fallo de comunicacion con la api");
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
