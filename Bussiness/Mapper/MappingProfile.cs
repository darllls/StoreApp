using AutoMapper;
using DataContext.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.CustomerType.TypeName))
                .ReverseMap();

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store != null ? src.Store.StoreName : null))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Store != null && src.Store.City != null ? src.Store.City.Name : null))
                .ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.BrandName : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory != null ? src.ProductCategory.CategoryName : null))
                .ReverseMap();

            CreateMap<AvailableProduct, AvailableProductDTO>()
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store != null ? src.Store.StoreName : null))
                .ReverseMap();

            CreateMap<AttributeProduct, AttributeDTO>()
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.Attribute != null ? src.Attribute.AttributeName : null))
                .ForMember(dest => dest.AttributeValue, opt => opt.MapFrom(src => src.AttributeValue != null ? src.AttributeValue.AttributeValue1 : null))
                .ReverseMap();

            CreateMap<AttributeDTO, DataContext.Models.Attribute>()
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.AttributeName))
                .ReverseMap();
        }
    }
}
