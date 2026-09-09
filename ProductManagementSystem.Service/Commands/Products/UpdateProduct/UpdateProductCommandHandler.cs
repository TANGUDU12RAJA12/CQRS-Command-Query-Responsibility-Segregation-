using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.ApplicationBbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IUpdateProductCommandHandler
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(AppDbContext appDbContext , IMapper mapper) 
        { 
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async  Task<bool> HandleAsync(UpdateProductCommand command)
        {
            var product = await _appDbContext.Product_CQRS
                   .FirstOrDefaultAsync(p => p.Id == command.Id);

            if (product == null)
            {
                return false;
            }

            _mapper.Map(command, product);

            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
