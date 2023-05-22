using Cobaia.WebApi.Persistence;
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
                .WriteTo.File(new JsonFormatter(renderMessage: true), "log.json")
                .CreateLogger();

            // Wire up application parts
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<CobaiaWebApiContext>(x => x.UseInMemoryDatabase("CobaiaWebApi"));
            builder.Services.AddSingleton(Log.Logger);
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