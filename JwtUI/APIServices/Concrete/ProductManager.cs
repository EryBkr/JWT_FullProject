using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JwtUI.APIServices.Interfaces;
using JwtUI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JwtUI.APIServices.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;

        public ProductManager(IHttpContextAccessor accessor, HttpClient httpClient)
        {
            _accessor = accessor;
            _httpClient = httpClient;
        }

        public async Task AddAsync(ProductAdd model)
        {
          var token=_accessor.HttpContext.Session.GetString("token");

           if(!string.IsNullOrWhiteSpace(token)){
                _httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);

                 var jsonData = JsonConvert.SerializeObject(model);
                 var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response=await _httpClient.PostAsync("https://localhost:44302/api/products",stringContent);

            }
        }

        public async Task Delete(int id)
        {
            var token=_accessor.HttpContext.Session.GetString("token");

              if(!string.IsNullOrWhiteSpace(token)){
                _httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);
                var response=await _httpClient.DeleteAsync("https://localhost:44302/api/products/"+id);
            }
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            var token=_accessor.HttpContext.Session.GetString("token");

            if(!string.IsNullOrWhiteSpace(token)){
                _httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);
                var response=await _httpClient.GetAsync("https://localhost:44302/api/products");

                if(response.IsSuccessStatusCode){
                   var products=JsonConvert.DeserializeObject<List<ProductModel>>(await response.Content.ReadAsStringAsync());
                   return products;
                }
            }

            return null;
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            var token=_accessor.HttpContext.Session.GetString("token");

             if(!string.IsNullOrWhiteSpace(token)){
                _httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);
                var response=await _httpClient.GetAsync("https://localhost:44302/api/products/"+id);

                if(response.IsSuccessStatusCode){
                   var product=JsonConvert.DeserializeObject<ProductModel>(await response.Content.ReadAsStringAsync());
                   return product;
                }
            }

            return null;
        }

        public async Task UpdateAsync(ProductEdit model)
        {
          var token=_accessor.HttpContext.Session.GetString("token");

           if(!string.IsNullOrWhiteSpace(token)){
                _httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);

                 var jsonData = JsonConvert.SerializeObject(model);
                 var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response=await _httpClient.PutAsync("https://localhost:44302/api/products",stringContent);

            }
        }
    }
}