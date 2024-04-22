using Basket_Api.Middleware;
using Basket_Api.Repository;

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
            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            });
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
