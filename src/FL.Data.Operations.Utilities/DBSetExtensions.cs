using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FL.Data.Operations.Utilities
{
    public static class DBSetExtensions
    {
        public static IQueryable<TEntity> Page<TEntity>(this IQueryable<TEntity> entityList, int pageNumber, int pageSize) where TEntity : class
        {
            if (pageSize == 0 || pageNumber == 0)
                return entityList.AsQueryable();

            var setCount = entityList == null ? 0 :entityList.Count();

            if (setCount == 0 || setCount <= pageSize)
                return entityList.AsQueryable();

            var pageCount = (int)Math.Ceiling(setCount / (decimal)pageSize);

            if (pageCount < pageNumber)
                pageNumber = pageCount;

            return entityList.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
