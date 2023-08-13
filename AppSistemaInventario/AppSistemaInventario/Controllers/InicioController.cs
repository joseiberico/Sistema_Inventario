using AppSistemaInventario.Models;
using AppSistemaInventario.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AppSistemaInventario.Services.IServices;

namespace AppSistemaInventario.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService<Usuario> _usuarioService;

        public InicioController(IUsuarioService<Usuario> usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string username, string password)
        {
            try
            {
                var usuario = await _usuarioService.IniciarSession(username, password);
                HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorLogin"] = "Usuario o clave inválido";
                return RedirectToAction("IniciarSesion", "Inicio");

            }

        }        
    }
}
