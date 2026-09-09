using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.Commands.Products.Delete
{
    public interface IDeleteProductCommandHandler
    {
        Task<bool> HandleAsync(DeleteProductCommand command);

    }
}
