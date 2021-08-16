using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FL.ExpressionToSQL;
using FL.ExpressionToSQL.Formatters;
using System.Linq;
using NHibernate.Linq;
using FL.Data.Operations.Utilities;
using NHibernate.Util;

namespace FL.Data.Operations.NHibernate
{
    public class NHibernateOperationHandler<TEntity> : IOperationHandlerBase<TEntity>
         where TEntity : class
    {
        private ISession _session;
        private readonly SchemaFormatter _schemaFormatter;

        public NHibernateOperationHandler(ISession session, SchemaFormatter schemaFormatter)
        {
            _session = session;
            _schemaFormatter = schemaFormatter;
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var deleteString = typeof(TEntity).BuildDeleteStatement(expression, _schemaFormatter);
            using (var tsn = _session.BeginTransaction())
            {
                await _session.CreateSQLQuery(deleteString).ExecuteUpdateAsync().ConfigureAwait(false);
                await tsn.CommitAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _session.Query<TEntity>().Where(expression).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> PagingAsync(Expression<Func<TEntity, object>> orderBy, int pageNumber, int recordsPerPage, Expression<Func<TEntity, bool>> expression)
        {
            return await _session.QueryOver<TEntity>().Where(expression).OrderBy(orderBy).Asc.Skip((pageNumber - 1) * recordsPerPage).Take(recordsPerPage).ListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> PagingAsync(Expression<Func<TEntity, object>> orderBy, int pageNumber, int recordsPerPage)
        {
            return await _session.QueryOver<TEntity>().OrderBy(orderBy).Asc.Skip((pageNumber - 1)* recordsPerPage).Take(recordsPerPage).ListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            var selectStatement = typeof(TEntity).BuildSelectStatement(_schemaFormatter, expression);
            var result = await _session.CreateSQLQuery(selectStatement).AddEntity(typeof(TEntity)).SetMaxResults(1).ListAsync<TEntity>().ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<object> InsertAsync(TEntity entity)
        {
            object savedEntity;
            using (var tsn = _session.BeginTransaction())
            {
                savedEntity = await _session.SaveAsync(entity).ConfigureAwait(false);
                await tsn.CommitAsync().ConfigureAwait(false);
            }

            return savedEntity;
        }

        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression)
        {
            //var updateStatement = entity.BuildUpdateStatement(expression, false, _schemaFormatter);
            using (var tsn = _session.BeginTransaction())
            {
                await _session.MergeAsync(entity).ConfigureAwait(false);
                await tsn.CommitAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _session.Query<TEntity>().ToListAsync();
        }
    }
}
