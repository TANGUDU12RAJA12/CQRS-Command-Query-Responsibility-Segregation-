using MediatR;
using ProductManagementSystem.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.MediatR.Queries.Products.GetProductsById
{
    public class GetProductsByIdMediatRQuery : IRequest<ProductDto?>
    {
        public int Id { get; set;  }
    }
}
