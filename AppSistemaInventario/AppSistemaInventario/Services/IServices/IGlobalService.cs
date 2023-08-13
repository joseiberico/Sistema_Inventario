namespace ApiSistemaInventario.Services.iServices
{
    public interface IGlobalService<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> Create (T entity);
        Task<T> Update (T entity);
        Task<bool> Delete (int id);
    }
}
