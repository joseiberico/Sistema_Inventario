namespace ApiSistemaInventario.Services.iServices
{
    public interface IVista<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T entity);
    }
}
