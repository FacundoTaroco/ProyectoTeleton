using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Models.Filtros
{
    public class RecepcionistaAdminPacienteLogueado: ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("USR")))
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
            if (context.HttpContext.Session.GetString("TIPO") == "PACIENTE" || context.HttpContext.Session.GetString("TIPO") == "RECEPCIONISTA" || context.HttpContext.Session.GetString("TIPO") == "ADMIN")
            {
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
        }
    }
}
