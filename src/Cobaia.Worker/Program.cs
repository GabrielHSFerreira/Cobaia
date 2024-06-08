using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Cobaia.Worker
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
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

                return 0;
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "Fatal error crashed application.");
                return -1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}