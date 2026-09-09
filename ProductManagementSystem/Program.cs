using Asp.Versioning;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Api.Endpoints;
using ProductManagementSystem.Api.Endpoints.V1;
using ProductManagementSystem.Core.ApplicationBbContext;
using ProductManagementSystem.Service.Commands.Products.CreateProducts;
using ProductManagementSystem.Service.Commands.Products.Delete;
using ProductManagementSystem.Service.Commands.Products.UpdateProduct;
using ProductManagementSystem.Service.Mappings;
using ProductManagementSystem.Service.MediatR.Commands.Products.CreateProduct;
using ProductManagementSystem.Service.MediatR.Queries.Products.GetProducts;
using ProductManagementSystem.Service.Queries.Products.GetProductById;
using ProductManagementSystem.Service.Queries.Products.GetProducts;

namespace ProductManagementSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(CreateProductMediatRCommandHandler).Assembly);
            });

            builder.Services.AddScoped<ICreateProductCommandHandler, CreateProductCommandHandler>();
            builder.Services.AddScoped<IGetProductsQueryHandler, GetProductsQueryHandler>();
            builder.Services.AddScoped<IGetProductByIdQueryHandler, GetProductByIdQueryHandler>();
            builder.Services.AddScoped<IUpdateProductCommandHandler, UpdateProductCommandHandler>();
            builder.Services.AddScoped<IDeleteProductCommandHandler, DeleteProductCommandHandler>();


            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);

                options.AssumeDefaultVersionWhenUnspecified = false;

                options.ReportApiVersions = true;

                options.ApiVersionReader =
                    new UrlSegmentApiVersionReader();
            });


            var app = builder.Build();

            app.MapProductEndPoints();
            app.MapMediatRProductEndpoints();

            app.Run();
        }
    }
}
