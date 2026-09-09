using AutoMapper;
using MediatR;
using ProductManagementSystem.Core.ApplicationBbContext;
using ProductManagementSystem.Core.Domain.Entities;
using ProductManagementSystem.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.MediatR.Commands.Products.CreateProduct
{
    public class CreateProductMediatRCommandHandler : IRequestHandler<CreateProductMediatRCommand, int>
    {

      private readonly AppDbContext _appDbContext;
      private readonly IMapper _mapper;


      public CreateProductMediatRCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductMediatRCommand command, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(command);

            await _appDbContext.Product_CQRS.AddAsync(product, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return product.Id;

        }
    }
}
 