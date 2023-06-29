using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Cobaia.JobScheduler.Jobs
{
    internal class GreetingsJob : IJob
    {
        private readonly ILogger<GreetingsJob> _logger;

        public GreetingsJob(ILogger<GreetingsJob> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello, friend! Now is {DateTimeNow:O}", DateTime.UtcNow);

            return Task.CompletedTask;
        }
    }
}