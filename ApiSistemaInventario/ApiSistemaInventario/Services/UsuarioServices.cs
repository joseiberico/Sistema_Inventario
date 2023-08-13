using ApiSistemaInventario.Context;
using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services.iServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiSistemaInventario.Services
{
    public class UsuarioServices : IServiceGlobal<Usuario>
    {
        private readonly DbSet<Usuario> _dbSet;
        private readonly SistemaInventarioContext _context;

        public UsuarioServices(SistemaInventarioContext context)
        {
            _dbSet = context.Set<Usuario>();
            _context = context;
        }

        public async Task<Usuario> Create(Usuario entity)
        {
            Usuario usuario = new Usuario()
            {
                Nombre = entity.Nombre,
                Username = entity.Username,
                Password = entity.Password,
                Estado = entity.Estado
            };

            EntityEntry<Usuario> result = await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            Usuario entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _dbSet.FirstAsync(x => x.Id == id);
        }

        public async Task<Usuario> Update(Usuario entity)
        {
            var productos = await GetById(entity.Id);
            if (productos == null)
            {
                return null;
            }

            productos.Nombre = entity.Nombre;
            productos.Username = entity.Username;
            productos.Password = entity.Password;
            productos.Estado = entity.Estado;

            await _context.SaveChangesAsync();
            return productos;
        }

        public async Task<Usuario> GetUsuarioById(string username)
        {
            return await _dbSet.FirstAsync(x => x.Username == username);
        }
    }
}
