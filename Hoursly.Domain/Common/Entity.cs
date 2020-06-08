using System;

namespace Hoursly.Domain.Common
{
    public class Entity
    {
        protected Entity()
        {
            PublicId = Guid.NewGuid();
        }

        public Guid PublicId { get; set; }
    }
}