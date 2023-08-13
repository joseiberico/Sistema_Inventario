using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services.iServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntradaProductoController : Controller
    {
        private readonly IVista<EntradaProducto> _entradaProducto;

        public EntradaProductoController(IVista<EntradaProducto> entradaProducto)
        {
            _entradaProducto = entradaProducto;
        }

        [HttpGet]
        public async Task<IActionResult> ListEntradaProducto()
        {
            var result = await _entradaProducto.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _entradaProducto.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EntradaProducto))]
        public async Task<IActionResult> CrearEntradas(EntradaProducto entradaProducto)
        {
            EntradaProducto result = await _entradaProducto.Create(entradaProducto);
            return new CreatedResult($"https://localhost:7003/api/EntradaProducto/{result.Id}", null);
        }
    }
}
