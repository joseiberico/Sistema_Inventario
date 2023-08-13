using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AppSistemaInventario.Models;
using AppSistemaInventario.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace AppSistemaInventario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioService<Usuario> _usuarioService;

        public HomeController(ILogger<HomeController> logger, IUsuarioService<Usuario> usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session != null)
            {
                var usuarioJson = HttpContext.Session.GetString("Usuario");
                if (!string.IsNullOrEmpty(usuarioJson))
                {
                    var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);

                    ViewBag.NombreUsuario = usuario.Nombre;
                }
            }

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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("IniciarSesion", "Inicio");
        }
    }
}