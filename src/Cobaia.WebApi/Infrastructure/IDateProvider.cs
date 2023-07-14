using System;

namespace Cobaia.WebApi.Infrastructure
{
    public interface IDateProvider
    {
        DateTime CurrentUtcDate();
    }
}