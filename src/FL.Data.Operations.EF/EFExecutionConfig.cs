using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace FL.Data.Operations.EF
{
    public interface IEFExecutionConfig<TEntity, TEntityDTO> : IExecutionConfigBase<TEntity>
        where TEntity : class
        where TEntityDTO : class
    {
        DbSet<TEntity> EntityDbSet { set; get; }
    
    }

    public class EFExecutionConfig<TEntity> : IEFExecutionConfig<TEntity>
        where TEntity : class
    {
  
        public EFExecutionConfig(  DbSet<TEntity> entityDbSet) 
           
        {
            EntityDbSet = entityDbSet;
        }

        public EFExecutionConfig( DbSet<TEntity> entityDbSet, IMapper mapper)
            : this(  entityDbSet)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; set; }
    
        public DbSet<TEntity> EntityDbSet { set; get; }
    }
}
