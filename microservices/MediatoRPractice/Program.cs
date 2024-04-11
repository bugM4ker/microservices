using Microsoft.Extensions.DependencyInjection;
using MediatoRPractice.Product;
using MediatR;
using System.Reflection;
using static MediatoRPractice.MediatorPattern.MediatorPattern;
using static MediatoRPractice.Product.ProductHandlerQuery;

namespace MediatoRPractice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddSingleton<IMediatorCustom, SimpleMediator>();
            builder.Services.AddTransient<IRequestHandlerCustom<GetProductQuery, GetProductDto>, CreateProductCommandHandler>();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
