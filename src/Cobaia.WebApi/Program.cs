using Cobaia.Persistence.Contexts;
using Cobaia.WebApi.Filters;
using Cobaia.WebApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace Cobaia.WebApi
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog();
                builder.Services.AddControllers(x => x.Filters.Add<ExceptionActionFilter>());
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddDbContext<CobaiaContext>(x => x.UseInMemoryDatabase("CobaiaWebApi"));
                builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
                builder.Services.AddSingleton<IDateProvider, HostDateProvider>();

                var app = builder.Build();

                app.UseSerilogRequestLogging();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseAuthorization();
                app.MapControllers();

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