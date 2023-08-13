using ApiSistemaInventario.Services.iServices;
using AppSistemaInventario.Models;
using AppSistemaInventario.Services;
using AppSistemaInventario.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AppSistemaInventario.Controllers
{
    public class EntradaProductoController : Controller
    {
        private readonly IVistaService<EntradaProducto> _service;
        private readonly IUsuarioService<Usuario> _usuario;
        private readonly IGlobalService<Producto> _producto;

        public EntradaProductoController(IVistaService<EntradaProducto> service, IUsuarioService<Usuario> usuario, IGlobalService<Producto> producto)
        {
            _service = service;
            _usuario = usuario;
            _producto = producto;
        }

        public async Task<IActionResult> Entrada()
        {
            var Entradas = await _service.GetAll();
            return View(Entradas);
        }

        public async Task<IActionResult> AgregarEntrada()
        {
            var Usuarios = await _usuario.GetAll();
            var Productos = await _producto.GetAll();
            ViewBag.Usuarios = Usuarios;
            ViewBag.Productos = Productos;

            return View(new EntradaProducto()); // Pasa una instancia de Producto vacía
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEntrada(EntradaProducto Entrada)
        {
            if (ModelState.IsValid)
            {
                var productoAgregado = await _service.Create(Entrada);
                if (productoAgregado == null)
                {
                    return RedirectToAction("Entrada", "EntradaProducto");
                }
                else
                {
                    ModelState.AddModelError("", "Error al agregar la entrada.");
                }
            }

            
            var Usuarios = await _usuario.GetAll();
            var Productos = await _producto.GetAll();
            ViewBag.Usuarios = Usuarios;           
            ViewBag.Productos = Productos;

            return View(Entrada);
        }
    }
}
