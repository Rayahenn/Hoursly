using System;
using Hoursly.Common.Decorators;

namespace Hoursly.Models
{
    public class BaseModel : IUniqueIdentifier
    {
        public BaseModel(Guid publicId)
        {
            PublicId = publicId;
        }

        public Guid PublicId { get; set; }
    }
}