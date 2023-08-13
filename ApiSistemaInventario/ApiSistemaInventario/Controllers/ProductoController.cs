using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services;
using ApiSistemaInventario.Services.iServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private readonly IServiceGlobal<Producto> _productoService;

        public ProductoController(IServiceGlobal<Producto> productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListUsuarios()
        {
            var result = await _productoService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id)
        {
            var result = await _productoService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Producto))]
        public async Task<IActionResult> AddProductos(Producto producto)
        {
            Producto result = await _productoService.Create(producto);
            return new CreatedResult($"https://localhost:7003/api/Producto/{result.Id}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _productoService.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Producto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProducto(Producto producto)
        {
            Producto? result = await _productoService.Update(producto);
            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}
