using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.Servicios;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.CasosUso.TotemCU;
using NuGet.Protocol.Plugins;
using LogicaNegocio.InterfacesDominio;
using LogicaAplicacion.CasosUso.UsuarioCU;
using AppTeleton.Worker;

using LogicaAplicacion.CasosUso.MedicoCU;
using LogicaAplicacion.CasosUso.AccesoTotemCU;
using LogicaAplicacion.CasosUso.CitaCU;
using LogicaAplicacion.CasosUso.DispositivoUsuarioCU;
using LogicaAplicacion.CasosUso.NotificacionCU;
using LogicaAplicacion.CasosUso.PreguntasFrecCU;
using AppTeleton.Hubs;
using LogicaAplicacion.CasosUso.ChatCU;
using System.Text.Json.Serialization;
using LogicaAplicacion.CasosUso.EncuestaCU;
using System.Globalization;
using Microsoft.AspNetCore.Localization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(
    option =>
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    ); ;
builder.Services.AddSignalR();

builder.Services.AddDbContext<LibreriaContext>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000000); // aca es el tiempo que la sesion se mantiene abierta ver que hacer pq el totem se tiene que                                                
    options.Cookie.HttpOnly = true;                      //mantener abierto indefinidamente para el totem
    options.Cookie.IsEssential = true;
});
//scopes de repositorios

builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

builder.Services.AddScoped<IRepositorioPaciente, RepositorioPaciente>();
builder.Services.AddScoped<IRepositorioMedico, RepositorioMedico>();
builder.Services.AddScoped<IRepositorioRecepcionista, RepositorioRecepcionista>();
builder.Services.AddScoped<IRepositorioAdministrador, RepositorioAdministrador>();
builder.Services.AddScoped<IRepositorioTotem, RepositorioTotem>();
builder.Services.AddScoped<IRepositorioAccesoTotem, RepositorioAccesoTotem>();
builder.Services.AddScoped<IRepositorioDispositivoNotificaciones, RepositorioDispositivoNotificaciones>();
builder.Services.AddScoped<IRepositorioNotificacion, RepositorioNotificacion>();
builder.Services.AddScoped<IRepositorioPreguntaFrec, RepositorioPreguntaFrec>();

builder.Services.AddScoped<IRepositorioChat, RepositorioChat>();
builder.Services.AddScoped<IRepositorioRespuestasEquivocadas, RepositorioRespuestaEquivocada>();

builder.Services.AddScoped<IRepositorioEncuesta, RepositorioEncuesta>();
//Scope de casos de uso

builder.Services.AddScoped<GetUsuarios, GetUsuarios>();
builder.Services.AddScoped<CambiarContrasenia, CambiarContrasenia>();

builder.Services.AddScoped<ABMPacientes, ABMPacientes>();
builder.Services.AddScoped<GetPacientes, GetPacientes>();
builder.Services.AddScoped<ActualizarPacientes, ActualizarPacientes>();

builder.Services.AddScoped<BorrarDispositivoNotificacion, BorrarDispositivoNotificacion>();
builder.Services.AddScoped<GuardarDispositivoNotificacion, GuardarDispositivoNotificacion>();
builder.Services.AddScoped<GetDispositivos, GetDispositivos>();

builder.Services.AddScoped<ABMRecepcionistas, ABMRecepcionistas>();
builder.Services.AddScoped<GetRecepcionistas, GetRecepcionistas>();

builder.Services.AddScoped<ABMAdministradores, ABMAdministradores>();
builder.Services.AddScoped<GetAdministradores, GetAdministradores>();

builder.Services.AddScoped<ABMTotem, ABMTotem>();
builder.Services.AddScoped<GetTotems, GetTotems>();
builder.Services.AddScoped<GenerarAvisoLlegada, GenerarAvisoLlegada>();


builder.Services.AddScoped<AccesoCU, AccesoCU>();

builder.Services.AddScoped<GetCitas, GetCitas>();

builder.Services.AddScoped<ABMMedicos, ABMMedicos>();
builder.Services.AddScoped<GetMedicos, GetMedicos>();

builder.Services.AddScoped<GetNotificacion, GetNotificacion>();
builder.Services.AddScoped<ABNotificacion, ABNotificacion>();
builder.Services.AddScoped<NotificacionesAutomaticas, NotificacionesAutomaticas>();

builder.Services.AddScoped<ABMPreguntasFrec, ABMPreguntasFrec>();
builder.Services.AddScoped<GetPreguntasFrec, GetPreguntasFrec>();

builder.Services.AddScoped<ABMChat, ABMChat>();
builder.Services.AddScoped<GetChats, GetChats>();

builder.Services.AddScoped<ABRespuestasEquivocadas, ABRespuestasEquivocadas>();
builder.Services.AddScoped<GetRespuestasEquivocadas, GetRespuestasEquivocadas>();

builder.Services.AddScoped<AgregarEncuesta, AgregarEncuesta>();
builder.Services.AddScoped<GetEncuestas, GetEncuestas>();
builder.Services.AddScoped<EnviarEncuestas, EnviarEncuestas>();

//scopes de servicios
builder.Services.AddScoped<EnviarNotificacionService, EnviarNotificacionService>();
builder.Services.AddScoped<SolicitarPacientesService, SolicitarPacientesService>();
builder.Services.AddScoped<SolicitarCitasService, SolicitarCitasService>();
builder.Services.AddScoped<RecepcionarPacienteService, RecepcionarPacienteService>();
builder.Services.AddScoped<ChatBotService, ChatBotService>();
builder.Services.AddScoped<GeolocalizacionService, GeolocalizacionService>();
//Usuario
builder.Services.AddScoped<ILogin, Login>();


//Worker
builder.Services.AddHostedService<CargarPacientesWorker>();
builder.Services.AddHostedService<NotificacionesAutomaticasWorker>();
builder.Services.AddHostedService<EnviarEncuestasWorker>();

var defaultCulture = new CultureInfo("es-AR"); 
defaultCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
};

var app = builder.Build();

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");


app.MapHub<ActualizarListadoHub>("/ActualizarListadoHub");
app.MapHub<HubConectado>("/ConnectedHub");
app.MapHub<PantallaLLamadosHub>("/PantallaLLamados");
app.MapHub<ListadoParaMedicosHub>("/ActualizarListadoParaMedicosHub");


app.Run();
