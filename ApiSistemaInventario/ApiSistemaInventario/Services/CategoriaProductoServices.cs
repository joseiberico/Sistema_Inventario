using ApiSistemaInventario.Context;
using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services.iServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiSistemaInventario.Services
{
    public class CategoriaProductoServices : IServiceGlobal<CategoriaProducto>
    {
        private readonly DbSet<CategoriaProducto> _dbSet;
        private readonly SistemaInventarioContext _context;

        public CategoriaProductoServices(SistemaInventarioContext context)
        {
            _dbSet = context.Set<CategoriaProducto>();
            _context = context;
        }

        public async Task<CategoriaProducto> Create(CategoriaProducto entity)
        {
            CategoriaProducto categoria = new CategoriaProducto()
            {
                Tipo = entity.Tipo
            };

            EntityEntry<CategoriaProducto> result = await _context.CategoriaProductos.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            CategoriaProducto entity = await GetById(id);
            if(entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoriaProducto>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<CategoriaProducto> GetById(int id)
        {
            return await _dbSet.FirstAsync(x => x.Id == id);
        }

        public Task<CategoriaProducto> GetUsuarioById(string usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoriaProducto> Update(CategoriaProducto entity)
        {
            var categoria = await GetById(entity.Id);
            if(categoria == null)
            {
                return null;
            }

            categoria.Tipo = entity.Tipo;

            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}
