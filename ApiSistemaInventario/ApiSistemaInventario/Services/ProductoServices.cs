using ApiSistemaInventario.Context;
using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services.iServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.RegularExpressions;

namespace ApiSistemaInventario.Services
{
    public class ProductoServices : IServiceGlobal<Producto>
    {
        private readonly DbSet<Producto> _dbSet;
        private readonly SistemaInventarioContext _context;

        public ProductoServices(SistemaInventarioContext context)
        {
            _dbSet = context.Set<Producto>();
            _context = context;
        }

        public async Task<Producto> Create(Producto entity)
        {
            Producto producto = new Producto()
            {
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                Precio = entity.Precio,
                Stock = entity.Stock,
                Marca = entity.Marca,
                Idcategoria = entity.Idcategoria,
                IdcategoriaNavigation = entity.IdcategoriaNavigation
            };

            EntityEntry<Producto> result = await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            Producto entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Producto>> GetAll()
        {
            return await _dbSet.Include(c => c.IdcategoriaNavigation).ToListAsync();
        }

        public async Task<Producto> GetById(int id)
        {
            return await _dbSet.FirstAsync(x => x.Id == id);
        }

        public Task<Producto> GetUsuarioById(string usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Producto> Update(Producto entity)
        {
            var producto = await GetById(entity.Id);

            if (producto == null)
            {
                return null;
            }

            producto.Nombre = entity.Nombre;
            producto.Descripcion = entity.Descripcion;
            producto.Precio = entity.Precio;
            producto.Stock = entity.Stock;
            producto.Marca = entity.Marca;
            producto.Idcategoria = entity.Idcategoria;
            producto.IdcategoriaNavigation = entity.IdcategoriaNavigation;
            

            await _context.SaveChangesAsync();
            return producto;

        }
    }
}
