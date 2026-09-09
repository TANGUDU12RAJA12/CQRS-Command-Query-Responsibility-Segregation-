using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Core.Domain.Entities;
using ProductManagementSystem.Service.Commands.Products.CreateProducts;
using ProductManagementSystem.Service.Commands.Products.Delete;
using ProductManagementSystem.Service.Commands.Products.UpdateProduct;
using ProductManagementSystem.Service.Queries.Products.GetProductById;
using ProductManagementSystem.Service.Queries.Products.GetProducts;
using System.Runtime.CompilerServices;

namespace ProductManagementSystem.Api.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndPoints( this WebApplication app)
        {
            //Create 
            app.MapPost("/api/products", async (
                [FromBody] CreateProductCommand command,
                [FromServices] ICreateProductCommandHandler handler) =>
            {
                var productId = await handler.HandleAsync(command);

                return Results.Ok(productId);
            });
            //Get All
            app.MapGet("/api/products",async(
            IGetProductsQueryHandler handler) =>
            {
                var products = await handler.HandleAsync(new GetProductQuery());
                return Results.Ok(products);
            });
            //Get BY ID 
            app.MapGet("/api/products/{id:int}", async (
               int id,
               IGetProductByIdQueryHandler handler) =>
            {
                var query = new GetProductByIdQuery
                {
                    Id = id
                };

                var product =
                    await handler.HandleAsync(query);

                if (product == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(product);
            });

            // Update 
            app.MapPut("/api/products/{id:int}" , async (
            int id,
            UpdateProductCommand command,
            IUpdateProductCommandHandler handler) =>
            {
                command.Id = id;

                var updated = await handler.HandleAsync(command);

                if (!updated)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            });

            //Delete
            app.MapDelete("/api/products/{id:int}", async (
            int id ,
            IDeleteProductCommandHandler handler) => 
            {
               var command = new DeleteProductCommand
               {
                   Id = id
               };
                var deleted = await handler.HandleAsync(command);
                if(!deleted)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });
        }
    }
}
