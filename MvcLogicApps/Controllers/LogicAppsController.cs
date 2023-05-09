using Microsoft.AspNetCore.Mvc;
using MvcLogicApps.Services;

namespace MvcLogicApps.Controllers
{
    public class LogicAppsController : Controller
    {
        private ServiceLogicApps serivce;

        public LogicAppsController(ServiceLogicApps serivce)
        {
            this.serivce = serivce;
        }
        

        public IActionResult Mail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Mail
            (string email, string asunto, string mensaje)
        {
            await this.serivce.SendMailAsync(email, asunto, mensaje);
            ViewData["MENSAJE"] = "Procesando Mail Logic Apps!!!";
            return View();
        }

    }
}
