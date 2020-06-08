using System;

namespace Hoursly.Domain.Common
{
    public class AuditableEntity : Entity
    {
        public AuditableEntity(IDateTimeProvider dateTimeProvider)
        {
            DateCreated = dateTimeProvider.Now;
        }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}