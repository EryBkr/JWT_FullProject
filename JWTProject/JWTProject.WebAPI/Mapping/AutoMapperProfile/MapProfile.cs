using AutoMapper;
using JWTProject.Entities.Concrete;
using JWTProject.Entities.DTO.ProductDtos;
using JWTProject.Entities.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTProject.WebAPI.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AddProductDto, Product>();
            CreateMap<Product, AddProductDto>();

            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, UpdateProductDto>();

            CreateMap<AppUser, UserRegisterDto>();
            CreateMap<UserRegisterDto, AppUser>();
        }
    }
}
