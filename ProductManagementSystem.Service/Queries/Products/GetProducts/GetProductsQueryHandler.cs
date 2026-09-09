using AutoMapper;
using ProductManagementSystem.Core.ApplicationBbContext;
using ProductManagementSystem.Service.DTOs;


using Microsoft.EntityFrameworkCore;

namespace ProductManagementSystem.Service.Queries.Products.GetProducts
{
    public class GetProductsQueryHandler : IGetProductsQueryHandler
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public  async Task<List<ProductDto>> HandleAsync(GetProductQuery query)
        {
           var products = await _appDbContext.Product_CQRS.AsNoTracking().ToListAsync();
            return _mapper.Map<List<DTOs.ProductDto>>(products);
        }
    }
}
