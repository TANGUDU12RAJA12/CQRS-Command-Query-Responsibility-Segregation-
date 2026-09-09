using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.ApplicationBbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.MediatR.Commands.Products.DeleteProduct
{
    public class DeleteProductMediatRCommandHandler : IRequestHandler<DeleteProductMediatRCommand, bool>
    {

        private readonly AppDbContext _appDbContext;
         private readonly IMapper _mapper;

        public DeleteProductMediatRCommandHandler(AppDbContext appDbContext ,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductMediatRCommand request, CancellationToken cancellationToken)
        {
            var product = await _appDbContext.Product_CQRS.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (product == null)
            {
                return false;
            }

            _appDbContext.Product_CQRS.Remove(product);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
