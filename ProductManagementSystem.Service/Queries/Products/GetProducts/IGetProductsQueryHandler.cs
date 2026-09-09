using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementSystem.Service.DTOs;
namespace ProductManagementSystem.Service.Queries.Products.GetProducts
{
    public  interface IGetProductsQueryHandler
    {
      Task<List<ProductDto>> HandleAsync(GetProductQuery query);
    }
}
