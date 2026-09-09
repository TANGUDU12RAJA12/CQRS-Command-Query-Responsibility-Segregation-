using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.ApplicationBbContext;
using ProductManagementSystem.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.MediatR.Queries.Products.GetProductsById
{
    public class GetProductsByIdMediatRQueryHandler : IRequestHandler<GetProductsByIdMediatRQuery, ProductDto?>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;


        public GetProductsByIdMediatRQueryHandler(AppDbContext appDbContext ,IMapper mapper){
         _appDbContext = appDbContext;
         _mapper = mapper;
        }
        public async Task<ProductDto?> Handle(GetProductsByIdMediatRQuery request, CancellationToken cancellationToken)
        {
            var product = await _appDbContext.Product_CQRS.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
            {
                return null;

            }

            return _mapper.Map<ProductDto>(product);
             
        }
            
    }
}
