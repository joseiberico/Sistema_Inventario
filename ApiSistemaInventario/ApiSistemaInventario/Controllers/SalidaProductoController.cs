using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services.iServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalidaProductoController : Controller
    {
        private readonly IVista<SalidaProducto> _salidaProducto;

        public SalidaProductoController(IVista<SalidaProducto> salidaProducto)
        {
            _salidaProducto = salidaProducto;
        }

        [HttpGet]
        public async Task<IActionResult> ListSalidaProducto()
        {
            var result = await _salidaProducto.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id)
        {
            var result = await _salidaProducto.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SalidaProducto))]
        public async Task<IActionResult> CrearSalidas(SalidaProducto salidaProducto)
        {
            SalidaProducto result = await _salidaProducto.Create(salidaProducto);
            return new CreatedResult($"https://localhost:7003/api/SalidaProducto/{result.Id}", null);
        }

    }
}
