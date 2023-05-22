using Quartz;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Cobaia.JobScheduler.Jobs
{
    internal class GreetingsJob : IJob
    {
        private readonly ILogger _logger;

        public GreetingsJob(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.Information("Hello, friend! Now is {DateTimeNow:O}", DateTime.UtcNow);

            return Task.CompletedTask;
        }
    }
}