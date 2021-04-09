using AutoMapper;
using JWTProject.Business.Abstracts;
using JWTProject.Business.StringInfos;
using JWTProject.Entities.Concrete;
using JWTProject.Entities.DTO.ProductDtos;
using JWTProject.WebAPI.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = RoleInfos.Admin+","+RoleInfos.Member)] //Admin veya Member erişimi gereklidir
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Product>))] //DI ile çalışması için bu şekilde yazdık + olarak Generic Tipi tanımladık
        [Authorize(Roles = RoleInfos.Admin + "," + RoleInfos.Member)] //Admin veya Member erişimi gereklidir
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidModel] //Validasyon kontrolünü ActionFilter ile yapıyoruz
        [Authorize(Roles =RoleInfos.Admin)] //Admin erişimi gereklidir
        public async Task<IActionResult> Add(AddProductDto model)
        {
            await _productService.AddAsync(_mapper.Map<Product>(model));
            return Created("", model);
        }

        [HttpPut]
        [ValidModel] //Validasyon kontrolünü ActionFilter ile yapıyoruz
        [Authorize(Roles =RoleInfos.Admin)] //Admin erişimi gereklidir
        public async Task<IActionResult> Update(UpdateProductDto product)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(product));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidId<Product>))] //DI ile çalışması için bu şekilde yazdık + olarak Generic Tipi tanımladık
        [Authorize(Roles = RoleInfos.Admin)] //Admin erişimi gereklidir
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return NoContent();
        }


       
    }
}
