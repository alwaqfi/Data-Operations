﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FL.Data.Operations
{
    public interface IEntityMapper
    {
        TDestination Map<TDestination>(object entity)
            where TDestination : class;
        Expression<Func<TDestination, bool>> Map<TDestination, TSource>(Expression<Func<TSource, bool>> expression)
            where TDestination : class
            where TSource : class;
    }
}
