using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services;
using ApiSistemaInventario.Services.iServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IServiceGlobal<Usuario> _UsuarioServices;
        private readonly IAutorizacionService _AutorizacionServices;

        public UsuarioController(IServiceGlobal<Usuario> usuarioServices, IAutorizacionService autorizacionServices)
        {
            _UsuarioServices = usuarioServices;
            _AutorizacionServices = autorizacionServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _UsuarioServices.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _UsuarioServices.GetById(id);
            return Ok(result);
        }

        [HttpGet("Usuario")]
        public async Task<IActionResult> GetUsuarioById(string usuario)
        {
            var result = await _UsuarioServices.GetUsuarioById(usuario);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Usuario))]
        public async Task<IActionResult> AddUsuario(Usuario usuario)
        {
            Usuario result = await _UsuarioServices.Create(usuario);
            return new CreatedResult($"https://localhost:7003/api/Usuario/{result.Id}", null);
        }
     
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var result = await _UsuarioServices.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Usuario))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUsuario(Usuario usuario)
        {
            Usuario result = await _UsuarioServices.Update(usuario);
            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            var resultado_autorizacion = await _AutorizacionServices.DevolverToken(autorizacion);
            if (resultado_autorizacion == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(resultado_autorizacion);
            }
        }
    }
}
