using Cobaia.Persistence.Contexts;
using Cobaia.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Cobaia.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Create default logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(renderMessage: true), ".log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Wire up application parts
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(x => x.Filters.Add<ExceptionActionFilter>());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<CobaiaContext>(x => x.UseInMemoryDatabase("CobaiaWebApi"));
            builder.Services.AddSingleton(Log.Logger);
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
            builder.Host.UseSerilog();

            // Build requests pipeline
            var app = builder.Build();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            // Start application
            app.Run();

            Log.CloseAndFlush();
        }
    }
}