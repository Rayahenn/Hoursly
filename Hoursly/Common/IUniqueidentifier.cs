using System;

namespace Hoursly.Common
{
    public interface IUniqueIdentifier
    {
        Guid PublicId { get; }
    }
}
