using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Cobaia.Worker
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(Log.Logger);
                services.AddMassTransit(configurator =>
                {
                    configurator.UsingRabbitMq((context, config) =>
                    {
                    });
                });
            });

            builder.UseSerilog();

            var app = builder.Build();
            app.Run();

            Log.CloseAndFlush();
        }
    }
}