using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.Commands.Products.UpdateProduct
{
    public interface IUpdateProductCommandHandler
    {
        Task<bool> HandleAsync(UpdateProductCommand command);
    }
}
