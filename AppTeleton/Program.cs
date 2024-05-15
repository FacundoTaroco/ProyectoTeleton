using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.PacienteCU;
using LogicaNegocio.InterfacesRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibreriaContext>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


//scopes de repositorios

builder.Services.AddScoped<IRepositorioPaciente, RepositorioPaciente>();

//Scope de casos de uso

builder.Services.AddScoped<ABMPacientes, ABMPacientes>();
builder.Services.AddScoped<GetPacientes, GetPacientes>();


//scopes de servicios




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
