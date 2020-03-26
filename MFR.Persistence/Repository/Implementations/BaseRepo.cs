using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository.Implementations
{
    public abstract class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class 
    {
        protected readonly DbContext _context;

        public BaseRepo(DbContext context) => _context = context;

        public async Task Add(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public async Task<ICollection<TEntity>> FindByCondition(Expression<Func<TEntity, bool>> predicate) 
            => await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();

        public void Remove(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public void RemoveRange(ICollection<TEntity> entities) => _context.Set<TEntity>().RemoveRange(entities);
    }
}
