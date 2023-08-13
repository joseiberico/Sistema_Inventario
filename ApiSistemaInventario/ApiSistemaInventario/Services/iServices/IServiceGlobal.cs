namespace ApiSistemaInventario.Services.iServices
{
    public interface IServiceGlobal<T>
    {
        Task<T> GetById(int id);
        Task<T> GetUsuarioById(string usuario);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create (T entity);
        Task<T> Update (T entity);
        Task<bool> Delete (int id);
    }
}
