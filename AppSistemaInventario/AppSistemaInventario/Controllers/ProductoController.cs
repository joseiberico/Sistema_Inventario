using ApiSistemaInventario.Services.iServices;
using AppSistemaInventario.Models;
using AppSistemaInventario.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSistemaInventario.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IGlobalService<Producto> _productoService;
        private readonly IGlobalService<CategoriaProducto> _categoriaService;

        public ProductoController(IGlobalService<Producto> productoService, IGlobalService<CategoriaProducto> categoriaService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> ListarProducto()
        {
            var productos = await _productoService.GetAll();
            return View(productos);
        }

        public async Task<IActionResult> AgregarProducto()
        {
            var Categorias = await _categoriaService.GetAll();
            ViewBag.Categorias = Categorias;
            return View(new Producto()); // Pasa una instancia de Producto vacía
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProducto(Producto producto)
        {
            if (ModelState.IsValid)
            {
                var productoAgregado = await _productoService.Create(producto);
                if (productoAgregado == null)
                {
                    return RedirectToAction("ListarProducto", "Producto");
                }
                else
                {
                    ModelState.AddModelError("", "Error al agregar el producto.");
                }
            }

            var Categorias = await _categoriaService.GetAll();
            ViewBag.Categorias = Categorias;

            return View(producto);
        }


        public async Task<IActionResult> ActualizarProducto(int id)
        {
            var Producto = await _productoService.GetById(id);
            if (Producto == null)
            {
                // La categoría no existe, puedes manejarlo de acuerdo a tus necesidades
                return RedirectToAction("Producto");
            }
            var Categorias = await _categoriaService.GetAll();
            ViewBag.Categorias = Categorias;
            return View(Producto);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarActualizarProducto(Producto Producto)
        {
            var ProductoActualizada = await _productoService.Update(Producto);
            if (ProductoActualizada != null)
            {
                return RedirectToAction("ListarProducto");
            }
            else
            {
                // Error al actualizar la categoría, puedes manejarlo de acuerdo a tus necesidades
                ModelState.AddModelError("", "Error al actualizar la categoría.");
                return View(Producto);
            }
        }

        public async Task<IActionResult> EliminarProducto(int id)
        {
            var Producto = await _productoService.GetById(id);
            if (Producto == null)
            {
                // La categoría no existe, puedes manejarlo de acuerdo a tus necesidades
                return RedirectToAction("Producto");
            }

            return View(Producto);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarEliminarProducto(int id)
        {
            var ProductoEliminada = await _productoService.Delete(id);
            if (ProductoEliminada)
            {
                return RedirectToAction("ListarProducto");
            }
            else
            {
                // Error al eliminar la categoría, puedes manejarlo de acuerdo a tus necesidades
                ModelState.AddModelError("", "Error al eliminar el producto.");
                return RedirectToAction("Producto");
            }
        }
    }
}
