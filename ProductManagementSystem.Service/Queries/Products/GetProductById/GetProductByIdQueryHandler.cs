using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.ApplicationBbContext;
using ProductManagementSystem.Service.DTOs;
using ProductManagementSystem.Service.Queries.Products.GetProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.Queries.Products.GetProductById
{
    public class GetProductByIdQueryHandler : IGetProductByIdQueryHandler
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(AppDbContext appDbContext  , IMapper mapper)
        {
            _appContext = appDbContext;
            _mapper = mapper;

        }
       

        public async Task<DTOs.ProductDto?> HandleAsync(GetProductByIdQuery query)
        {
            var product = await _appContext.Product_CQRS.FirstOrDefaultAsync(p => p.Id == query.Id);
            return _mapper.Map<DTOs.ProductDto>(product);
        }
    }
}
