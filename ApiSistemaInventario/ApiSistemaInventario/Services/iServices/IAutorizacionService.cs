using ApiSistemaInventario.Models;

namespace ApiSistemaInventario.Services.iServices
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
    }
}
