using System;

namespace Hoursly.Domain.Common
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}