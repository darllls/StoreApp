using AutoMapper;
using DataContext.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<ProductCategory, CategoryDTO>().ReverseMap();
            CreateMap<CustomerType, CustomerTypeDTO>().ReverseMap();
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
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null))
                .ReverseMap();

            CreateMap<AttributeProduct, AttributeDTO>()
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.Attribute != null ? src.Attribute.AttributeName : null))
                .ForMember(dest => dest.AttributeValue, opt => opt.MapFrom(src => src.AttributeValue != null ? src.AttributeValue.AttributeValue1 : null))
                .ReverseMap();

            CreateMap<AttributeDTO, DataContext.Models.Attribute>()
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.AttributeName))
                .ReverseMap();

            CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : null))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : null))
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Employee != null && src.Employee.Store != null ? src.Employee.Store.StoreName : null))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Employee != null && src.Employee.Store != null && src.Employee.Store.City != null ? src.Employee.Store.City.Name : null))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ReverseMap()
            .ForMember(dest => dest.Customer, opt => opt.Ignore()) // Ignore mapping for Customer and Employee
            .ForMember(dest => dest.Employee, opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.AvailableProduct != null && src.AvailableProduct.Product != null ? src.AvailableProduct.Product.ProductName : null))
                .ReverseMap();

            CreateMap<AvailableProduct, AvailableProductDTO>()
                .ForMember(dest => dest.AvailableProductId, opt => opt.MapFrom(src => src.AvailableProductId))
                .ReverseMap();



        }
    }
}
