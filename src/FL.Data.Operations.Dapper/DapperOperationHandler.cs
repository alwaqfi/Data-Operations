using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using FL.ExpressionToSQL;
using FL.ExpressionToSQL.Formatters;

namespace FL.Data.Operations.Dapper
{
    public class DapperOperationHandler<TEntityDTO> : IOperationHandlerBase<TEntityDTO>, IDisposable
         where TEntityDTO : class
    {
        private readonly DbConnection _dbConnection;
        private readonly SchemaFormatter _schemaFormatter;

        public DapperOperationHandler(DbConnection dbConnection, SchemaFormatter schemaFormatter)
        {
            _dbConnection = dbConnection;
            _schemaFormatter = schemaFormatter;
        }
        public async Task DeleteAsync(Expression<Func<TEntityDTO, bool>> expression)
        {
            //   using (var connect = await Connect())
            //  {
            var command = typeof(TEntityDTO).BuildDeleteStatement(expression, _schemaFormatter);
            await _dbConnection.ExecuteAsync(command).ConfigureAwait(false);
            //   }
        }

        public async Task<IEnumerable<TEntityDTO>> FindAsync(Expression<Func<TEntityDTO, bool>> expression)
        {
            //   using (var connect = await Connect())
            //   {
            var command = typeof(TEntityDTO).BuildSelectStatement(_schemaFormatter, expression);
            return await _dbConnection.QueryAsync<TEntityDTO>(command).ConfigureAwait(false);
            //  }
        }

        public async Task<IEnumerable<TEntityDTO>> GetAllAsync()
        {
            //using (var connect = await Connect())
            //{
            var command = typeof(TEntityDTO).BuildSelectStatement<TEntityDTO>(_schemaFormatter);
            return await _dbConnection.QueryAsync<TEntityDTO>(command).ConfigureAwait(false);
            //}
        }

        public async Task<TEntityDTO> GetAsync(Expression<Func<TEntityDTO, bool>> expression)
        {
            //  using (var connect = await Connect())
            //   {
            var command = typeof(TEntityDTO).BuildSelectStatement(_schemaFormatter, expression);
            return await _dbConnection.QueryFirstOrDefaultAsync<TEntityDTO>(command);
            //   }
        }

        public async Task<object> InsertAsync(TEntityDTO entity)
        {
            // using (var connect = await Connect())
            //     {
            var command = entity.BuildInsertStatement<TEntityDTO>(false, _schemaFormatter);
            return await _dbConnection.ExecuteAsync(command).ConfigureAwait(false);
            // }
        }

        public async Task<IEnumerable<TEntityDTO>> PagingAsync(Expression<Func<TEntityDTO, object>> orderBy, int pageNumber, int recordsPerPage, Expression<Func<TEntityDTO, bool>> expression)
        {
            var command = typeof(TEntityDTO).BuildSelectStatement<TEntityDTO>(orderBy, (pageNumber - 1) * recordsPerPage , recordsPerPage, _schemaFormatter, expression);
            //   using (var connect = await Connect())
            //  {
            return await _dbConnection.QueryAsync<TEntityDTO>(command).ConfigureAwait(false);
            //  }
        }

        public async Task<IEnumerable<TEntityDTO>> PagingAsync(Expression<Func<TEntityDTO, object>> orderBy, int pageNumber, int recordsPerPage)
        {
            //    using (var connect = await Connect())
            //   {
            var command = typeof(TEntityDTO).BuildSelectStatement<TEntityDTO>(orderBy, (pageNumber - 1) * recordsPerPage , recordsPerPage, _schemaFormatter);
            //   using (var connect = await Connect())
            //  {
            return await _dbConnection.QueryAsync<TEntityDTO>(command).ConfigureAwait(false);
            //   }
        }

        public async Task UpdateAsync(TEntityDTO entity, Expression<Func<TEntityDTO, bool>> expression)
        {
            //  using (var connect = await Connect())
            //   {
            var command = entity.BuildUpdateStatement<TEntityDTO>(expression, false, _schemaFormatter);
            await _dbConnection.ExecuteAsync(command).ConfigureAwait(false);
            //  }
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }

        //private async Task<DbConnection> Connect()
        //{
        //    if (_dbConnection.State != ConnectionState.Open)
        //    {
        //        await _dbConnection.OpenAsync();
        //    }
        //    return _dbConnection;
        //}
    }
}
