using ApiSistemaInventario.Services.iServices;
using AppSistemaInventario.Models;
using AppSistemaInventario.Services;
using AppSistemaInventario.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AppSistemaInventario.Controllers
{
    public class SalidaProductoController : Controller
    {
        private readonly IVistaService<SalidaProducto> _service;
        private readonly IUsuarioService<Usuario> _usuario;
        private readonly IGlobalService<Producto> _producto;

        public SalidaProductoController(IVistaService<SalidaProducto> service, IUsuarioService<Usuario> usuario, IGlobalService<Producto> producto)
        {
            _service = service;
            _usuario = usuario;
            _producto = producto;
        }

        public async Task<IActionResult> Salida()
        {
            var Salidas = await _service.GetAll();
            return View(Salidas);
        }

        public async Task<IActionResult> AgregarSalida()
        {
            var Usuarios = await _usuario.GetAll();
            var Productos = await _producto.GetAll();
            ViewBag.Usuarios = Usuarios;
            ViewBag.Productos = Productos;

            return View(new SalidaProducto());
        }

        [HttpPost]
        public async Task<IActionResult> AgregarSalida(SalidaProducto Salida)
        {
            if (ModelState.IsValid)
            {
                var productoAgregado = await _service.Create(Salida);
                if (productoAgregado == null)
                {
                    return RedirectToAction("Salida", "SalidaProducto");
                }
                else
                {
                    ModelState.AddModelError("", "Error al agregar la salida.");
                }
            }


            var Usuarios = await _usuario.GetAll();
            var Productos = await _producto.GetAll();
            ViewBag.Usuarios = Usuarios;
            ViewBag.Productos = Productos;

            return View(Salida);
        }
    }
}
