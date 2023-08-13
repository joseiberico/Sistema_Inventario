using ApiSistemaInventario.Services.iServices;
using AppSistemaInventario.Models;
using AppSistemaInventario.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSistemaInventario.Controllers
{
    public class CategoriaProductoController : Controller
    {
        private readonly IGlobalService<CategoriaProducto> _categoriaService;

        public CategoriaProductoController(IGlobalService<CategoriaProducto> categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Categoria()
        {
            var categorias = await _categoriaService.GetAll();
            return View(categorias);
        }

        public IActionResult AgregarCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCategoria(CategoriaProducto categoria)
        {

            var categoriaAgregado = await _categoriaService.Create(categoria);
            if (categoriaAgregado != null)
            {
                return View();
            }

            return RedirectToAction("Categoria", "CategoriaProducto");
        }


        public async Task<IActionResult> ActualizarCategoria(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            if (categoria == null)
            {
                // La categoría no existe, puedes manejarlo de acuerdo a tus necesidades
                return RedirectToAction("Categoria");
            }

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarActualizarCategoria(CategoriaProducto categoria)
        {
            var categoriaActualizada = await _categoriaService.Update(categoria);
            if (categoriaActualizada != null)
            {
                return RedirectToAction("Categoria");
            }
            else
            {
                // Error al actualizar la categoría, puedes manejarlo de acuerdo a tus necesidades
                ModelState.AddModelError("", "Error al actualizar la categoría.");
                return View(categoria);
            }
        }

        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            if (categoria == null)
            {
                // La categoría no existe, puedes manejarlo de acuerdo a tus necesidades
                return RedirectToAction("Categoria");
            }

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarEliminarCategoria(int id)
        {
            var categoriaEliminada = await _categoriaService.Delete(id);
            if (categoriaEliminada)
            {
                return RedirectToAction("Categoria");
            }
            else
            {
                // Error al eliminar la categoría, puedes manejarlo de acuerdo a tus necesidades
                ModelState.AddModelError("", "Error al eliminar la categoría.");
                return RedirectToAction("Categoria");
            }
        }
    }
}
