using ApiSistemaInventario.Context;
using ApiSistemaInventario.Models;
using ApiSistemaInventario.Services.iServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiSistemaInventario.Services
{
    public class SalidaProductoServices : IVista<SalidaProducto>
    {
        private readonly DbSet<SalidaProducto> _dbSet;
        private readonly SistemaInventarioContext _context;

        public SalidaProductoServices(SistemaInventarioContext context)
        {
            _dbSet = context.Set<SalidaProducto>();
            _context = context;
        }

        public async Task<IEnumerable<SalidaProducto>> GetAll()
        {
            return await _dbSet
                .Include(c => c.IdproductoNavigation)
                .Include(d => d.IdUsuarioNavigation)
                .ToListAsync();
        }

        public async Task<SalidaProducto> GetById(int id)
        {
            return await _dbSet.FirstAsync(x => x.Id == id);
        }

        public async Task<SalidaProducto> Create(SalidaProducto entity)
        {
            SalidaProducto salidaProducto = new SalidaProducto()
            {
                Idproducto = entity.Idproducto,
                Descripcion = entity.Descripcion,
                StockS = entity.StockS,
                IdUsuario = entity.IdUsuario
            };

            EntityEntry<SalidaProducto> result = await _context.SalidaProductos.AddAsync(salidaProducto);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
