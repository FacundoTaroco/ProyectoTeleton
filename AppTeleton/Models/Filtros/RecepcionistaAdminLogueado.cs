using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Models.Filtros
{
    public class RecepcionistaAdminLogueado : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("USR")))
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
            if (context.HttpContext.Session.GetString("TIPO") == "ADMIN" || context.HttpContext.Session.GetString("TIPO") == "RECEPCIONISTA")
            {
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
        }
    }
}
