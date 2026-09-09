using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Service.MediatR.Commands.Products.CreateProduct;
using ProductManagementSystem.Service.MediatR.Commands.Products.DeleteProduct;
using ProductManagementSystem.Service.MediatR.Commands.Products.UpdateProduct;
using ProductManagementSystem.Service.MediatR.Queries.Products.GetProductById;
using ProductManagementSystem.Service.MediatR.Queries.Products.GetProducts;

namespace ProductManagementSystem.Api.Endpoints.V1
{
    public static class MediatRProductEndpoints
    {
        public static void MapMediatRProductEndpoints(
               this WebApplication app)

        {


            var versionedApi =
               app.NewVersionedApi("ProductManagement");

            var v1 =
                versionedApi
                    .MapGroup("/api/v{version:apiVersion}/products")
                    .HasApiVersion(1.0);

            v1.MapGet("/", async (
                 IMediator mediator) =>
            {
                var query = new GetProductsMediatRQuery();
                var products = await mediator.Send(query);
                return Results.Ok(products);
            });

            v1.MapGet("/{id:int}", async (
                int id,
                 IMediator mediator) =>
            {
                var query = new GetProductByIdMediatRQuery
                {
                    Id = id
                };

                var product = await mediator.Send(query);
                if (product == null)
                    return Results.NotFound();

                return Results.Ok(product);
            });

            v1.MapPost("/", async (
                [FromBody] CreateProductMediatRCommand command,
                [FromServices] IMediator mediator) =>
            {
                var productId = await mediator.Send(command);
                return Results.Ok(productId);
            });

            v1.MapPut("/{id:int}", async (
                 int id,
                 UpdateProductMediatRCommand command,
                 IMediator mediator) =>
            {
                command.Id = id;

                var updated = await mediator.Send(command);

                if (!updated)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            });

            v1.MapDelete("/{id:int}", async (
                int id,
                 IMediator mediator) =>
            {
                var command = new DeleteProductMediatRCommand
                {
                    Id = id
                };

                var deleted = await mediator.Send(command);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            });
        }
    }
}
