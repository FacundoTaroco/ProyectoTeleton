using LogicaAplicacion.CasosUso.PacienteCU;

namespace AppTeleton.Worker
{
    public class NativeWorker:BackgroundService
    {
        //Este es un programa que queda corriendo en segundo plano y permite actualizar los pacientes una vez cada el tiempo que queramoos

        private readonly ILogger<NativeWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public NativeWorker(ILogger<NativeWorker> logger, IServiceProvider serviceProvider /*notificaciones automaticas*/)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {

                using (IServiceScope scope = _serviceProvider.CreateScope()) {
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
                    
                    //delay en milisegundos entre que se ejecuta una tarea y otra
                    await Task.Delay(/*86400000*/15000, stoppingToken);
                }



                /* _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);*/


                
            }
        }
    }
}
