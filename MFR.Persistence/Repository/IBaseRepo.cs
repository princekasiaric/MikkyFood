using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IBaseRepo<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(ICollection<TEntity> entities);
        Task<ICollection<TEntity>> FindByCondition(Expression<Func<TEntity, bool>> predicate);
    }
}
