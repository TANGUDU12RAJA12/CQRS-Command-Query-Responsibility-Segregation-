using ProductManagementSystem.Core.ApplicationBbContext;
using ProductManagementSystem.Core.Domain.Entities;
using AutoMapper;
namespace ProductManagementSystem.Service.Commands.Products.CreateProducts
{
    public  class CreateProductCommandHandler : ICreateProductCommandHandler
    {
       private readonly AppDbContext _appDbContext;
       private readonly IMapper _mapper;
        public CreateProductCommandHandler( AppDbContext appDbContext, IMapper mapper)
       {
       _appDbContext = appDbContext;
       _mapper = mapper;
       }

        public async Task<int> HandleAsync(CreateProductCommand command)
        {
            //var product = new Product
            //{
            //    Name = command.Name,
            //    Description = command.Description,
            //    Price = command.Price,
            //
            //   Stock = command.Stock,
            //};

            var product = _mapper.Map<Product>(command);

            _appDbContext.Product_CQRS.Add(product);
            await _appDbContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
