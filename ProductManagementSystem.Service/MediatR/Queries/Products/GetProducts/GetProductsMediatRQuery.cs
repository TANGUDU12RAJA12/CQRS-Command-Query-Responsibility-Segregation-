using MediatR;
using ProductManagementSystem.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Service.MediatR.Queries.Products.GetProducts
{
    public class GetProductsMediatRQuery :IRequest<List<DTOs.ProductDto>>
    {
    }
}
