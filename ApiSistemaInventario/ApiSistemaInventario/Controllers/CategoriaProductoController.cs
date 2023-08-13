using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services;
using ApiSistemaInventario.Services.iServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaProductoController : Controller
    {
        private readonly IServiceGlobal<CategoriaProducto> _categoriaServices;

        public CategoriaProductoController(IServiceGlobal<CategoriaProducto> categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        [HttpGet]
        public async Task<IActionResult> ListaCategorias()
        {
            var categorias = await _categoriaServices.GetAll();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoriaServices.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoriaProducto))]
        public async Task<IActionResult> CrearCategorias(CategoriaProducto categoria)
        {
            CategoriaProducto result = await _categoriaServices.Create(categoria);
            return new CreatedResult($"https://localhost:7003/api/CategoriaProducto/{result.Id}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var result = await _categoriaServices.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoriaProducto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFamilia(CategoriaProducto categoria)
        {
            CategoriaProducto result = await _categoriaServices.Update(categoria);
            if (result == null)
            
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}
