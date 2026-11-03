using Microsoft.AspNetCore.Mvc;
using OCCODE_CON.Models; // Cambia "TuProyecto" por el nombre de tu namespace

namespace OCCODE_CON.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly SqlServerContext _context;

        public SeguridadController(SqlServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Busca al usuario en la BD generada por el Scaffold
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Username == username && u.PasswordHash == password && u.Estado == true);

            if (usuario != null)
            {
                // Guardamos los datos en la sesión
                HttpContext.Session.SetString("UsuarioLogueado", usuario.Username);
                HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                HttpContext.Session.SetInt32("IdPerfil", usuario.IdPerfil);

                return RedirectToAction("Index", "Home");
            }

            // Si falla, mandamos un mensaje de error a la vista
            TempData["Error"] = "Usuario o contraseña incorrectos, o cuenta inactiva.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}