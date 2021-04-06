using JWTProject.DataAccess.Abstracts;
using JWTProject.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JWTProject.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfGenericRepository<TEntity> : IGenericDal<TEntity> where TEntity:class,IEntity,new()
    {
        public async Task AddAsync(TEntity entity)
        {
            using var context = new JwtContext();
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            using var context = new JwtContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new JwtContext();
            return await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new JwtContext();
            return await context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            using var context = new JwtContext();
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            using var context = new JwtContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using var context = new JwtContext();
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
