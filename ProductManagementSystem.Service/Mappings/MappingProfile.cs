using AutoMapper;
using ProductManagementSystem.Core.Domain.Entities;
using ProductManagementSystem.Service.Commands.Products.CreateProducts;
using ProductManagementSystem.Service.Commands.Products.UpdateProduct;
using ProductManagementSystem.Service.DTOs;
using ProductManagementSystem.Service.MediatR.Commands.Products.CreateProduct;
using ProductManagementSystem.Service.MediatR.Commands.Products.UpdateProduct;

namespace ProductManagementSystem.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<CreateProductMediatRCommand, Product>().ReverseMap();
            CreateMap<UpdateProductMediatRCommand, Product>().ReverseMap();
        }
    }
}
