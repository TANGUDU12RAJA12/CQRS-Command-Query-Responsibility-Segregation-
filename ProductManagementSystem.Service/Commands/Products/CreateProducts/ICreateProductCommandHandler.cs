using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.Commands.Products.CreateProducts
{
    public interface ICreateProductCommandHandler
    {
    Task<int> HandleAsync(CreateProductCommand command);
    }
}
