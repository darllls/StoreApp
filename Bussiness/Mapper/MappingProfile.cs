using AutoMapper;
using DataContext.Models;
using DataContextWH.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<ProductCategory, CategoryDTO>().ReverseMap();
            CreateMap<CustomerType, CustomerTypeDTO>().ReverseMap();
            CreateMap<SupplyStatus, SupplyStatusDTO>().ReverseMap();
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
            CreateMap<AttributeProduct, AttributeDTO>()
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.Attribute != null ? src.Attribute.AttributeName : null))
                .ForMember(dest => dest.AttributeValue, opt => opt.MapFrom(src => src.AttributeValue != null ? src.AttributeValue.AttributeValue1 : null))
                .ReverseMap();

            CreateMap<AttributeDTO, DataContext.Models.Attribute>()
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.AttributeName))
                .ReverseMap();

            CreateMap<AvailableProduct, AvailableProductDTO>()
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store != null ? src.Store.StoreName : null))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null))
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

            CreateMap<Supply, SupplyDTO>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.StatusName))
                .ReverseMap()
                .ForMember(dest => dest.Supplier, opt => opt.Ignore());

            CreateMap<Supplier, SupplierDTO>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                .ReverseMap();

            CreateMap<SupplyDetail, SupplyDetailsDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ReverseMap()
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<ShipmentInvoice, ShipmentInvoiceDTO>()
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.AvailableProduct.Store.StoreName))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ReverseMap()
                .ForMember(dest => dest.AvailableProduct, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.SupplyDetails, opt => opt.Ignore());

            CreateMap<SupplyDetailsFact, SupplyDetailsFactDTO>()
                .ForMember(dest => dest.SupplyNumber, opt => opt.MapFrom(src => src.Supply != null ? src.Supply.SupplyNumber : null))
                .ForMember(dest => dest.SupplyDate, opt => opt.MapFrom(src => src.Supply != null && src.Supply.SupplyDate != null ?
                    new DateTime(src.Supply.SupplyDate.DateYear ?? 1, src.Supply.SupplyDate.DateMonth ?? 1, src.Supply.SupplyDate.DateDay ?? 1) : (DateTime?)null))
                .ForMember(dest => dest.SupplyStatus, opt => opt.MapFrom(src => src.Supply != null ? src.Supply.SupplyStatus : null))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null))
                .ForMember(dest => dest.ProductCategoryConcat, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductCategoryConcat : null))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product != null ? src.Product.Brand : null))
                .ReverseMap();

            CreateMap<OrderDetailsFact, OrderDetailsFactDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null))
                .ForMember(dest => dest.ProductCategoryConcat, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductCategoryConcat : null))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product != null ? src.Product.Brand : null))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FirstName + " " + src.Employee.LastName : null))
                .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.CustomerType != null ? src.CustomerType.TypeName : null))
                .ForMember(dest => dest.DiscountRate, opt => opt.MapFrom(src => src.CustomerType != null ? src.CustomerType.DiscountRate : null))
                .ForMember(dest => dest.PeriodStartDate, opt => opt.MapFrom(src => src.PeriodStart != null ? new DateTime(src.PeriodStart.DateYear ?? 1, src.PeriodStart.DateMonth ?? 1, src.PeriodStart.DateDay ?? 1) : (DateTime?)null))
                .ForMember(dest => dest.PeriodEndDate, opt => opt.MapFrom(src => src.PeriodEnd != null ? new DateTime(src.PeriodEnd.DateYear ?? 1, src.PeriodEnd.DateMonth ?? 1, src.PeriodEnd.DateDay ?? 1) : (DateTime?)null))
                .ReverseMap();
        }
    }
}
