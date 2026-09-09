using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.ApplicationBbContext;
using ProductManagementSystem.Service.DTOs;

namespace ProductManagementSystem.Service.MediatR.Queries.Products.GetProductById
{
    public class GetProductByIdMediatRQueryHandler : IRequestHandler<GetProductByIdMediatRQuery, ProductDto?>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetProductByIdMediatRQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Handle(GetProductByIdMediatRQuery request, CancellationToken cancellationToken)
        {
            var product = await _appDbContext.Product_CQRS
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return _mapper.Map<ProductDto?>(product);
        }
    }
}
