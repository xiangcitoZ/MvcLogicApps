using Microsoft.AspNetCore.Mvc;
using MvcLogicApps.Services;

namespace MvcLogicApps.Controllers
{
    public class ServiceBusController : Controller
    {
        private ServiceQueueBus service;

        public ServiceBusController(ServiceQueueBus service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            Index(string mensaje, string accion)
        {
            if (accion.ToLower() == "enviar")
            {
                await this.service.SendMessageAsync(mensaje);
                ViewData["MENSAJE"] = "Mensaje enviado al Topic!!!";
                return View();
            }
            else
            {
                List<string> mensajes =
                    await this.service.ReceiveMessagesAsync();
                ViewData["MENSAJE"] = "Recibiendo mensajes: " + mensajes.Count;
                return View(mensajes);
            }
        }

    }
}
