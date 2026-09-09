using AutoMapper;
using MediatR;
using ProductManagementSystem.Core.ApplicationBbContext;
using ProductManagementSystem.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ProductManagementSystem.Service.MediatR.Queries.Products.GetProducts
{
    public class GetProductsMediatRQueryHandler : IRequestHandler<GetProductsMediatRQuery, List<DTOs.ProductDto>>
    {

       private readonly AppDbContext _appDbContext;
       private readonly IMapper _mapper;

       public GetProductsMediatRQueryHandler(AppDbContext appDbContext, IMapper mapper)
       {
         _appDbContext = appDbContext;
         _mapper = mapper;
        }
        public  async Task<List<DTOs.ProductDto>> Handle(GetProductsMediatRQuery request, CancellationToken cancellationToken)
        {
            var products = await _appDbContext.Product_CQRS
                           .AsNoTracking()
                           .ToListAsync(cancellationToken);

            return _mapper.Map<List<DTOs.ProductDto>>(products);
        }
    }
}
