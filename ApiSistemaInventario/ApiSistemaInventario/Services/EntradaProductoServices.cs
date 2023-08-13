using ApiSistemaInventario.Context;
using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services.iServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiSistemaInventario.Services
{
    public class EntradaProductoServices : IVista<EntradaProducto>
    {
        private readonly DbSet<EntradaProducto> _dbSet;
        private readonly SistemaInventarioContext _context;

        public EntradaProductoServices(SistemaInventarioContext context)
        {
            _dbSet = context.Set<EntradaProducto>();
            _context = context;
        }

        public async Task<IEnumerable<EntradaProducto>> GetAll()
        {
            return await _dbSet
                .Include(c => c.IdproductoNavigation)
                .Include(d => d.IdUsuarioNavigation)
                .ToListAsync();
        }

        public async Task<EntradaProducto> GetById(int id)
        {
            return await _dbSet.FirstAsync(x => x.Id == id);
        }

        public async Task<EntradaProducto> Create(EntradaProducto entity)
        {
            EntradaProducto entradaProducto = new EntradaProducto()
            {
                Idproducto = entity.Idproducto,
                Descripcion = entity.Descripcion,
                StockE = entity.StockE,
                IdUsuario = entity.IdUsuario,
                IdUsuarioNavigation = entity.IdUsuarioNavigation,
                IdproductoNavigation = entity.IdproductoNavigation
                
            };

            EntityEntry<EntradaProducto> result = await _context.EntradaProductos.AddAsync(entradaProducto);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

       
    }
}
