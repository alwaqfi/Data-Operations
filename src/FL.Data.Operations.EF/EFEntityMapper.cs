using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FL.Data.Operations.EF
{
    public class EFEntityMapper : IEntityMapper
    {
        public IMapper Mapper { set; get; }
        public EFEntityMapper(IMapper mapper)
        {
            Mapper = mapper;
        }

        public TDestination Map<TDestination>(object entity) where TDestination : class
        {
            return Mapper.Map<TDestination>(entity);
        }
        public Expression<Func<TDestination, bool>> Map<TDestination, TSource>(Expression<Func<TSource, bool>> expression) where TDestination : class where TSource : class
        {
            return Mapper.Map<Expression<Func<TDestination, bool>>>(expression);
        }
    }
}
