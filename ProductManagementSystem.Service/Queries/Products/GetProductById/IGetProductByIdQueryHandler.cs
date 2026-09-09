using ProductManagementSystem.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.Queries.Products.GetProductById
{
    public interface IGetProductByIdQueryHandler
    {
        Task<ProductDto?> HandleAsync(GetProductByIdQuery query);
    }
}
