using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace XDev_Model.Interfaces
{
    public interface IGenericEntity<TEntity> where TEntity : class
    {
        /// <summary>
        /// Obtiene el primer registro o el predeterminado de una entidad
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<TEntity> GetFirstorDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Obtiene el primer registro o el predeterminado de una entidad
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetFirstorDefaultAsync();

        /// <summary>
        /// Consulta para entidad
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Obtiene entidad única por llave primaria
        /// </summary>
        /// <param name="id">ID tipo string</param>
        /// <returns>Retorna entidad</returns>
        Task<TEntity> GetByIdAsync(params object[] id);

        /// <summary>
        /// Crea una entidad en la BD
        /// </summary>
        /// <param name="entity">Entidad a crear</param>
        /// <returns>Retorna entidad</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Actualiza una entidad en la BD
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Retorna entidad</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, string oldversion);
        Task<TEntity> GetFirstorDefaultAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task CreateAsync(params TEntity[] entity);
        Task<TEntity> GetFirstorDefaultAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    }
}
