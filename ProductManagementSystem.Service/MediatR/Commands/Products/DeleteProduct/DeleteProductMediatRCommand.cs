using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.MediatR.Commands.Products.DeleteProduct
{
    public class DeleteProductMediatRCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
