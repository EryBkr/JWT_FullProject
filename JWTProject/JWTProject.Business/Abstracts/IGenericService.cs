using JWTProject.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.Abstracts
{
    public interface IGenericService<TEntity> where TEntity:class,IEntity,new()
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
