using System;
using Hoursly.Common.Decorators;

namespace Hoursly.Common.Extensions.Models
{
    public static class ModelsExtensions
    {
        public static void ThrowIfPublicIdIsNullOrEmpty<TModel>(this TModel model) where TModel : IUniqueIdentifier
        {
            if (model.PublicId == Guid.Empty || model.PublicId == null)
                throw new InvalidOperationException($"{nameof(model)} {nameof(model.PublicId)} cannot be null");
        }
    }
}