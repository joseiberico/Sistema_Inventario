using AppSistemaInventario.Models;
using AppSistemaInventario.Services;
using AppSistemaInventario.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AppSistemaInventario.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IUsuarioService<Usuario> _usuarioService;

        public UsuarioController(IUsuarioService<Usuario> usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> ListaUsuario()
            {
                var usuarios = await _usuarioService.GetAll();
                return View(usuarios);
            }

            public async Task<IActionResult> Agregarusuario(Usuario usuario)
            {
                var usuarioAgregado = await _usuarioService.Create(usuario);
                if (usuarioAgregado != null)
                {
                    return RedirectToAction("Usuario");
                }
                else
                {
                    // Error al agregar el usuario, puedes manejarlo de acuerdo a tus necesidades
                    ModelState.AddModelError("", "Error al agregar la usuario.");
                    return View();
                }
            }

            public async Task<IActionResult> ActualizarUsuario(int id)
            {
                var usuario = await _usuarioService.GetById(id);
                if (usuario == null)
                {
                    // La categoría no existe, puedes manejarlo de acuerdo a tus necesidades
                    return RedirectToAction("Usuario");
                }

                return View(usuario);
            }

            [HttpPost]
            public async Task<IActionResult> ActualizarUsuario(Usuario usuario)
            {
                var usuarioActualizada = await _usuarioService.Update(usuario);
                if (usuarioActualizada != null)
                {
                    return RedirectToAction("Usuario");
                }
                else
                {
                    // Error al actualizar la categoría, puedes manejarlo de acuerdo a tus necesidades
                    ModelState.AddModelError("", "Error al actualizar el Usuario.");
                    return View(usuario);
                }
            }

            public async Task<IActionResult> EliminarUsuario(int id)
            {
                var usuario = await _usuarioService.GetById(id);
                if (usuario == null)
                {
                    // La categoría no existe, puedes manejarlo de acuerdo a tus necesidades
                    return RedirectToAction("Usuario");
                }

                return View(usuario);
            }

            [HttpPost]
            public async Task<IActionResult> ConfirmarEliminarusuario(int id)
            {
                var usuarioEliminada = await _usuarioService.Delete(id);
                if (usuarioEliminada)
                {
                    return RedirectToAction("Usuario");
                }
                else
                {
                    // Error al eliminar la categoría, puedes manejarlo de acuerdo a tus necesidades
                    ModelState.AddModelError("", "Error al eliminar el usuario.");
                    return RedirectToAction("Usuario");
                }
            }
    }
}
