﻿using AppTeleton.Models;
using AppTeleton.Models.Filtros;
using LogicaAplicacion.CasosUso.PreguntasFrecCU;
using LogicaAplicacion.Servicios;
using LogicaNegocio.DTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesWit;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Controllers
{
    public class PreguntasFrecuentesController : Controller
    {
        private readonly ABMPreguntasFrec _abmPreguntasFrec;
        private readonly GetPreguntasFrec _getPreguntasFrec;
        private readonly ChatBotService _chatBotService;


        public PreguntasFrecuentesController(ChatBotService chatBotService, GetPreguntasFrec getPreguntasFrec, ABMPreguntasFrec abmPreguntasFrec)
        {
            _getPreguntasFrec = getPreguntasFrec;
            _abmPreguntasFrec = abmPreguntasFrec;
            _chatBotService = chatBotService;
        }


        [RecepcionistaAdminLogueado]
        public IActionResult PreguntasFrecuentes()
        {
            var modelo = ObtenerModeloPreguntasFrec();
            ViewBag.IsAdminOrRecepcionista = User.IsInRole("Admin") || User.IsInRole("Recepcionista");
            return View(modelo);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _abmPreguntasFrec.BajaPreguntaFrec(id);
                return RedirectToAction("PreguntasFrecuentes", new { mensaje = "Pregunta frecuente eliminada temporalmente con éxito, se cargará del servidor central nuevamente en el próximo llamado ", tipoMensaje = "EXITO" });
            }
            catch (Exception)
            {
                return RedirectToAction("PreguntasFrecuentes", new { mensaje = "Error al eliminar pregunta frecuente", tipoMensaje = "ERROR" });
            }
        }

        [RecepcionistaAdminLogueado]
        [HttpGet]
        public IActionResult Create()
        {
           
            IEnumerable<CategoriaPregunta> categoriaPreguntas = _getPreguntasFrec.GetCategorias();
            AltaPreguntaViewModel model = new AltaPreguntaViewModel();  
            model.Categorias = categoriaPreguntas;



            return View(model);
            
           
         
        }

        [RecepcionistaAdminLogueado]
        [HttpPost]
        public IActionResult Create(string pregunta,string isChecked, string categoriaSeleccionada, string categoriaNueva ,  string respuesta, string categoriaNuevaDescripcion, bool paraTotem)
        {
            try
            {

                IEnumerable<CategoriaPregunta> Categorias = _getPreguntasFrec.GetCategorias();

                if (isChecked == "on") //En caso de que quiera crear una nueva categoria
                {
                    if (String.IsNullOrEmpty(categoriaNueva) || String.IsNullOrEmpty(respuesta))
                    {
                        throw new Exception("Ingrese el nombre de la categoria y la respuesta asociada a ella");
                    }
                    CategoriaPregunta? validarSiExiste = Categorias.FirstOrDefault(c => c.Categoria == categoriaNueva);
                    if (validarSiExiste != null)
                    {

                        throw new Exception("Ya existe una categoria con ese nombre, porfavor ingrese otro");
                    }

                    //primero tenemos que crear la nueva categoria, guardarla y enviarla como intent a wit
                    CategoriaPregunta nuevaCategoria = new CategoriaPregunta(categoriaNueva, respuesta, categoriaNuevaDescripcion);
                    _abmPreguntasFrec.AltaCategoria(nuevaCategoria);

                    //mandamos la categoria a wit
                    IntentDTO intent = new IntentDTO();
                    intent.name = categoriaNueva;
                    _chatBotService.PostIntent(intent);

                    //creamos la pregunta frecuente 
                    CategoriaPregunta categoria = _getPreguntasFrec.GetCategoriaPorNombre(categoriaNueva);
                    PreguntaFrec preguntaNueva = new PreguntaFrec(pregunta, categoria, paraTotem);
                    _abmPreguntasFrec.AltaPreguntaFrec(preguntaNueva);

                    //enviamos la pregunta como utterance a wit para que se entrene

                    Utterance utterance = new Utterance();
                    utterance.text = pregunta;
                    utterance.intent = categoriaNueva;
                    utterance.traits = new List<UtteranceTrait>();
                    utterance.entities = new List<UtteranceEntity>();
                    List<Utterance> utterances = new List<Utterance> { utterance };
                    _chatBotService.PostUtterance(utterances);

                    return RedirectToAction("PreguntasFrecuentes");
                }

                //La persona selecciona una categoria existente
                else {
                    if (categoriaSeleccionada.Equals("noSeleccionado")) {
                        throw new Exception("Seleccione una categoria");
                    }

                    //buscamos la categoria seleccionada
                    CategoriaPregunta categoria = _getPreguntasFrec.GetCategoriaPorNombre(categoriaSeleccionada);

                    //creamos la pregunta frecuente 
                   
                    PreguntaFrec preguntaNueva = new PreguntaFrec(pregunta, categoria,paraTotem);
                    _abmPreguntasFrec.AltaPreguntaFrec(preguntaNueva);

                    //enviamos la pregunta como utterance a wit para que se entrene
                    
                    Utterance utterance = new Utterance();
                    utterance.text = pregunta;
                    utterance.intent = categoriaSeleccionada;
                    utterance.traits = new List<UtteranceTrait>();
                    utterance.entities = new List<UtteranceEntity>();
                    List<Utterance> utterances = new List<Utterance> { utterance };
                    _chatBotService.PostUtterance(utterances);

                    return RedirectToAction("PreguntasFrecuentes");

                }
            }
            catch (Exception e) {


                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;

                IEnumerable<CategoriaPregunta> categoriaPreguntas = _getPreguntasFrec.GetCategorias();
                AltaPreguntaViewModel model = new AltaPreguntaViewModel();
                model.Categorias = categoriaPreguntas;
                return View(model);
            }
              
        }

        [RecepcionistaAdminLogueado]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var preguntaFrec = _getPreguntasFrec.GetPreguntaFrecPorId(id);
            if (preguntaFrec == null)
            {
                return NotFound();
            }
            return View(preguntaFrec);
        }

        [HttpPost]
        public IActionResult Edit(PreguntaFrec preguntaFrec)
        {
            try
            {
                _abmPreguntasFrec.ModificarPreguntaFrec(preguntaFrec);
                ViewBag.TipoMensaje = "EXITO";
                ViewBag.Mensaje = "Pregunta frecuente editada con éxito";
                return RedirectToAction("PreguntasFrecuentes");
            }
            catch (Exception e)
            {
                ViewBag.TipoMensaje = "ERROR";
                ViewBag.Mensaje = e.Message;
                return View(preguntaFrec);
            }
        }


        [RecepcionistaAdminLogueado]
        public IActionResult Detalle(int id)
        {
            var preguntaFrec = _getPreguntasFrec.GetPreguntaFrecPorId(id);
            return View(preguntaFrec);
        }


        private PreguntasFrecViewModel ObtenerModeloPreguntasFrec()
        {
            IEnumerable<PreguntaFrec> preguntasFrec = _getPreguntasFrec.GetAll();
            PreguntasFrecViewModel modeloIndex = new PreguntasFrecViewModel(preguntasFrec);
            return modeloIndex;
        }
    }
}
