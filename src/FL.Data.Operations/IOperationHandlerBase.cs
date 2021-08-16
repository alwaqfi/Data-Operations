using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FL.Data.Operations
{
    public interface IOperationHandlerBase<TDbSet, TEntityDTO>
      where TDbSet : class
      where TEntityDTO : class
    {
        Task<TEntityDTO> GetAsync(TDbSet dbSet, IEntityMapper mapper, Expression<Func<TEntityDTO, bool>> expression);
        Task<IEnumerable<TEntityDTO>> GetAllAsync(TDbSet dbSet, IEntityMapper mapper);
        Task<IEnumerable<TEntityDTO>> FindAsync(TDbSet dbSet, IEntityMapper mapper, Expression<Func<TEntityDTO, bool>> expression);
        Task<IEnumerable<TEntityDTO>> PagingAsync(TDbSet dbSet, IEntityMapper mapper, int pageNumber, int recordsPerPage, Expression<Func<TEntityDTO, bool>> expression);
        Task<IEnumerable<TEntityDTO>> PagingAsync(TDbSet dbSet, IEntityMapper mapper, int pageNumber, int recordsPerPage);
        Task DeleteAsync(TDbSet dbSet, IEntityMapper mapper, Expression<Func<TEntityDTO, bool>> expression);
        Task<TEntityDTO> InsertAsync(TDbSet dbSet, TEntityDTO entityDTO, IEntityMapper mapper);
        Task UpdateAsync(TDbSet dbSet, TEntityDTO entityDTO, IEntityMapper mapper, Expression<Func<TEntityDTO, bool>> expression);
        // Task<IEnumerable<TEntityDTO>> SearchAsync(IExecutionConfigBase<TEntity> executionConfigBase, IFetchConfigBase fetchConfigBase, Expression<Func<TEntityDTO, bool>> expression);
    }
     
    public interface IOperationHandlerBase<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> PagingAsync(Expression<Func<TEntity, object>> orderBy, int pageNumber, int recordsPerPage, Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> PagingAsync(Expression<Func<TEntity, object>> orderBy, int pageNumber, int recordsPerPage);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<object> InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression);
        // Task<IEnumerable<TEntityDTO>> SearchAsync(IExecutionConfigBase<TEntity> executionConfigBase, IFetchConfigBase fetchConfigBase, Expression<Func<TEntityDTO, bool>> expression);
    }
}
