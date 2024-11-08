using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class GenericEntity<TEntity> : IGenericEntity<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext dbContext;

        public GenericEntity(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbContext.Set<TEntity>().AnyAsync(filter);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task CreateAsync(params TEntity[] entity)
        {
            dbContext.Set<TEntity>().AddRange(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(params object[] ids)
        {
            return await dbContext.Set<TEntity>().FindAsync(ids);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, string oldversion)
        {
            dbContext.Entry(entity).Property("ConcurrencyStamp").OriginalValue = oldversion;
            dbContext.Entry(entity).State = EntityState.Modified;
            //Console.WriteLine(dbContext.ChangeTracker.DebugView.LongView);
            //dbContext.ChangeTracker.DetectChanges();
            //Console.WriteLine(dbContext.ChangeTracker.DebugView.LongView);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> GetFirstorDefaultAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetFirstorDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetFirstorDefaultAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            await Task.Run(() => {
                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = orderBy(query);
            });

            return query;
        }

        public async Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            await Task.Run(() => {
                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = orderBy(query);
            });

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query;
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetFirstorDefaultAsync()
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync();
        }
    }
}
