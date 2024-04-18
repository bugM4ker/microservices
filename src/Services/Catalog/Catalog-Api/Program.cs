using Marten;

namespace Catalog_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddValidators();
            builder.Services.AddControllers();
            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            });

            builder.Services.AddMarten(opts =>
            {
                opts.Connection(builder.Configuration.GetConnectionString("Catalog_Db")!);
            }).UseLightweightSessions();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ValidationMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
