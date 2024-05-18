using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaAplicacion.CasosUso.RecepcionistaCU;
using LogicaAplicacion.CasosUso.AdministradorCU;
using LogicaAplicacion.Servicios;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.CasosUso.TotemCU;
using NuGet.Protocol.Plugins;
using LogicaNegocio.InterfacesDominio;
using LogicaAplicacion.CasosUso.Usuario;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibreriaContext>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000000); // aca es el tiempo que la sesion se mantiene abierta ver que hacer pq el totem se tiene que                                                
    options.Cookie.HttpOnly = true;                      //mantener abierto indefinidamente
    options.Cookie.IsEssential = true;
});


//scopes de repositorios

builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

builder.Services.AddScoped<IRepositorioPaciente, RepositorioPaciente>();
builder.Services.AddScoped<IRepositorioRecepcionista, RepositorioRecepcionista>();
builder.Services.AddScoped<IRepositorioAdministrador, RepositorioAdministrador>();
builder.Services.AddScoped<IRepositorioTotem, RepositorioTotem>();
builder.Services.AddScoped<IRepositorioSesionTotem, RepositorioSesionTotem>();
builder.Services.AddScoped<IRepositorioAccesoTotem, RepositorioAccesoTotem>();
//Scope de casos de uso

builder.Services.AddScoped<ABMPacientes, ABMPacientes>();
builder.Services.AddScoped<GetPacientes, GetPacientes>();

builder.Services.AddScoped<ABMRecepcionistas, ABMRecepcionistas>();
builder.Services.AddScoped<GetRecepcionistas, GetRecepcionistas>();

builder.Services.AddScoped<ABMAdministradores, ABMAdministradores>();
builder.Services.AddScoped<GetAdministradores, GetAdministradores>();

builder.Services.AddScoped<ABMTotem, ABMTotem>();
builder.Services.AddScoped<GetTotems, GetTotems>();


//scopes de servicios

builder.Services.AddScoped<SolicitarPacientesService, SolicitarPacientesService>();
//Usuario
builder.Services.AddScoped<ILogin, Login>();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
