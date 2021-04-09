using System.Collections.Generic;
using System.Threading.Tasks;
using JwtUI.Models;

namespace JwtUI.APIServices.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllAsync();
        Task AddAsync(ProductAdd model);
        Task UpdateAsync(ProductEdit model);
        Task<ProductModel> GetByIdAsync(int id);
        Task Delete(int id);
    }
}