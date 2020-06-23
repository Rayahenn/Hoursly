using System;

namespace Hoursly.Common.Decorators
{
    public interface IUniqueIdentifier
    {
        Guid PublicId { get; }
    }
}