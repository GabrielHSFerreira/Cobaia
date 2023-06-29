using Cobaia.JobScheduler.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Serilog;
using Serilog.Formatting.Json;

namespace Cobaia.JobScheduler
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Create default logger
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(renderMessage: true), ".log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Wire up application parts
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(Log.Logger);
                services.AddQuartz(quartzConfigurator =>
                {
                    quartzConfigurator.UseMicrosoftDependencyInjectionJobFactory();

                    quartzConfigurator.ScheduleJob<GreetingsJob>(trigger =>
                        trigger.WithSimpleSchedule(x =>
                            x.WithIntervalInSeconds(5).RepeatForever()));
                });
                services.AddQuartzHostedService();
            });

            builder.UseSerilog();

            // Build application
            var app = builder.Build();

            // Start application
            app.Run();

            Log.CloseAndFlush();
        }
    }
}