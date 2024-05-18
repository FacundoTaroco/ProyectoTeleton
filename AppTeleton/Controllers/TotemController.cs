using AppTeleton.Models;
using LogicaNegocio.DTO;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace AppTeleton.Controllers
{
    public class TotemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string? error)
        {

            if (!String.IsNullOrEmpty(error))
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = error;

            }
            return View();


        }
        [HttpPost]
        public IActionResult Login(string nombre, string contra)
        {
            try
            {
                var options = new RestClientOptions("http://localhost:5223")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/Totems/login");
                request.AddHeader("Content-Type", "application/json");
                var body = new
                {

                    Nombre = nombre,
                    Contrasenia = contra

                };
                request.AddJsonBody(body);
                RestResponse response = client.ExecutePost(request);

                JsonSerializerOptions optionsJson = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                if (response.Content == null)
                {
                    throw new Exception("Error de comunicacion con la api");
                }
                HttpStatusCode res = response.StatusCode;
                if (res == HttpStatusCode.OK)
                {
                    TokenResponse token = JsonSerializer.Deserialize<TokenResponse>(response.Content, optionsJson);
                    HttpContext.Session.SetString("Usr", token.NombreUsuario);
                    HttpContext.Session.SetString("Token", token.token);
                    ViewBag.TipoMensaje = "EXITO";
                    ViewBag.Mensaje = "Bienvenido " + token.NombreUsuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Error error = JsonSerializer.Deserialize<Error>(response.Content, optionsJson);
                    throw new Exception("Error " + error.Code + " " + error.Details);
                }
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View("Login");
            }



        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.TipoMensaje = "ERROR";
            ViewBag.Mensaje = "Se cerro la sesion";
            return View("Login");
        }
    }
}



