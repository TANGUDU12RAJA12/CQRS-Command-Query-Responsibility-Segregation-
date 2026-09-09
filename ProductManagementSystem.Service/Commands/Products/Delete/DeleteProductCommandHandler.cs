using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.ApplicationBbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.Commands.Products.Delete
{
    public class DeleteProductCommandHandler : IDeleteProductCommandHandler
    {
          private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

         public DeleteProductCommandHandler( AppDbContext appDbContext ,IMapper mapper)
         {
             _appDbContext = appDbContext;
             _mapper = mapper;
         }
        public async Task<bool> HandleAsync(DeleteProductCommand command)
        {
            var product = await _appDbContext.Product_CQRS.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (product == null)
            {
                return false;

            }

            _appDbContext.Product_CQRS.Remove(product);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
