using System;

namespace Cobaia.WebApi.Infrastructure
{
    internal class HostDateProvider : IDateProvider
    {
        public DateTime CurrentUtcDate()
        {
            return DateTime.UtcNow;
        }
    }
}