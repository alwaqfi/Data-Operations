using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using FL.Data.Operations;

using FL.Data.Operations.Utilities;

namespace FL.Data.Operations.EF
{

    public class EFOperationHandler<TDbSet, TEntity, TEntityDTO> : IOperationHandlerBase<TDbSet, TEntityDTO>
        where TEntity : class
        where TDbSet : DbSet<TEntity>
        where TEntityDTO : class
    {
        private readonly DbContext _context;

        public EFOperationHandler(DbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(TDbSet dbSet, IEntityMapper mapper, Expression<Func<TEntityDTO, bool>> expression)
        {
            var exp = mapper.Map<TEntity, TEntityDTO>(expression);
            var list = await dbSet.Where(exp).ToListAsync().ConfigureAwait(false);
            dbSet.RemoveRange(list);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntityDTO>> FindAsync(TDbSet dbSet, IEntityMapper mapper, Expression<Func<TEntityDTO, bool>> expression)
        {
            var exp = mapper.Map<TEntity, TEntityDTO>(expression);
            var list = await dbSet.Where(exp).ToListAsync().ConfigureAwait(false);
            return mapper.Map<IEnumerable<TEntityDTO>>(list);
        }

        public async Task<IEnumerable<TEntityDTO>> PagingAsync(TDbSet dbSet, IEntityMapper mapper, int pageNumber, int recordsPerPage, Expression<Func<TEntityDTO, bool>> expression)
        {
            var exp = mapper.Map<TEntity, TEntityDTO>(expression);
            var list = await dbSet.Where(exp).Page(pageNumber, recordsPerPage).ToListAsync().ConfigureAwait(false);
            return mapper.Map<IEnumerable<TEntityDTO>>(list);
        }

        public async Task<IEnumerable<TEntityDTO>> PagingAsync(TDbSet dbSet, IEntityMapper mapper, int pageNumber, int recordsPerPage)
        {
            var list = await dbSet.Page(pageNumber, recordsPerPage).ToListAsync().ConfigureAwait(false);
            return mapper.Map<IEnumerable<TEntityDTO>>(list);
        }

        public async Task<TEntityDTO> GetAsync(TDbSet dbSet, IEntityMapper mapper, Expression<Func<TEntityDTO, bool>> expression)
        {
            var exp = mapper.Map<TEntity, TEntityDTO>(expression);
            var obj = await dbSet.FirstOrDefaultAsync(exp).ConfigureAwait(false);
            return mapper.Map<TEntityDTO>(obj);
        }

        public async Task<TEntityDTO> InsertAsync(TDbSet dbSet, TEntityDTO entityDTO, IEntityMapper mapper)
        {
            var entity = mapper.Map<TEntity>(entityDTO);
            dbSet.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<TEntityDTO>(entity);
        }

        public async Task UpdateAsync(TDbSet dbSet, TEntityDTO entityDTO, IEntityMapper mapper, Expression<Func<TEntityDTO, bool>> expression)
        {
            var entity = mapper.Map<TEntity>(entityDTO);
            var exp = mapper.Map<TEntity, TEntityDTO>(expression);
            var obj = await dbSet.FirstOrDefaultAsync(exp).ConfigureAwait(false);
            FieldMapper.Map(entity, obj);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntityDTO>> GetAllAsync(TDbSet dbSet, IEntityMapper mapper)
        {
            var list = await dbSet.ToListAsync().ConfigureAwait(false);
            return mapper.Map<IEnumerable<TEntityDTO>>(list);
        }
    }
}
