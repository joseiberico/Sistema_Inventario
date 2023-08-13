namespace AppSistemaInventario.Services.IServices
{
    public interface IUsuarioService<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
        Task<T> IniciarSession(string username, string password);
    }
}
