using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.ApplicationBbContext;

namespace ProductManagementSystem.Service.MediatR.Commands.Products.UpdateProduct
{
    public class UpdateProductMediatRCommandHandler : IRequestHandler<UpdateProductMediatRCommand, bool>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateProductMediatRCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductMediatRCommand request, CancellationToken cancellationToken)
        {
            var product = await _appDbContext.Product_CQRS
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product == null)
            {
                return false;
            }

            _mapper.Map(request, product);

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
