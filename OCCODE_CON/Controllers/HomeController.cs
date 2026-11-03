using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using OCCODE_CON.Models; // Ajusta si tu namespace es distinto

namespace OCCODE_CON.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Validamos si la sesión está vacía (nadie se ha logueado)
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                // Lo pateamos a la pantalla de Login
                return RedirectToAction("Login", "Seguridad");
            }

            // Si sí hay sesión, le pasamos el nombre a la vista para saludarlo
            ViewBag.Usuario = HttpContext.Session.GetString("UsuarioLogueado");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}