using JWTProject.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.DataAccess.Abstracts
{
    public interface IGenericDal<TEntity> where TEntity : class, IEntity, new()
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter);
        Task UpdateAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);

    }
}
