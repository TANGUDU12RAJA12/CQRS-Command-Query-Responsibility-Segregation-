using MediatR;
using ProductManagementSystem.Service.DTOs;

namespace ProductManagementSystem.Service.MediatR.Queries.Products.GetProductById
{
    public class GetProductByIdMediatRQuery : IRequest<ProductDto?>
    {
        public int Id { get; set; }
    }
}
