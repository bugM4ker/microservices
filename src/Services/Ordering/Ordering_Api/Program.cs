using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddInfrastructureServices(builder.Configuration)
                .AddApplicationServices(builder.Configuration)
                .AddApiServices();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseApiServices();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
