namespace ApiSistemaInventario.Services.iServices
{
    public interface IVistaService<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> Create(T entity);
    }
}
