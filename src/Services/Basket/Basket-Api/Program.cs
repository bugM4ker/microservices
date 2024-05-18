using Basket_Api.Middleware;
using Basket_Api.Models;
using Basket_Api.Repository;
using Discount_gRPC.Protos;
using Marten;
using Microsoft.Extensions.Caching.Distributed;
using BuildingBlocks.Messaging.MassTransit;
namespace Basket_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<IBasketRepository, BasketRepositoryBase>();
            builder.Services.Decorate<IBasketRepository, BasketRepositoryCachingDecorator>();
            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            });

            builder.Services.AddGrpcClient<DisCountProtoService.DisCountProtoServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
            });

            builder.Services.AddMarten(opts =>
            {
                opts.Connection(builder.Configuration.GetConnectionString("Basket_Db")!);
                opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
            }).UseLightweightSessions();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                options.InstanceName = "BasketCache";
            });

            builder.Services.AddMessageBroker(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionHandlerMiddlerware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
