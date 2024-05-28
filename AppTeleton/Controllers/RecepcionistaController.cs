﻿using AppTeleton.Models.Filtros;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    [RecepcionistaLogueado]
    public class RecepcionistaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
