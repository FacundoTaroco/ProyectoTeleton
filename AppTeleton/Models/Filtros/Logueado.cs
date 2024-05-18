
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AppTeleton.Models.Filtros
{
    public class Logueado : ActionFilterAttribute, IActionFilter
    {
        //este filtro cuando tengamos el session hecho hace automaticamente la verificacion de que tipo de usuario esta logueado
        //para evitar que accedan a funcionalidades que no les corresponden
        /*public override void OnActionExecuting(ActionExecutingContext context)
        {
             if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("USR")))
             {
                 context.Result = new RedirectToActionResult("Login", "Usuario", null);
             }
        }*/

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("USR")))
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
        }
    }
}
