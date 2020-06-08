using System;
using Hoursly.Domain.Common.SharedKernel;

namespace Hoursly.Domain.Common
{
    public class AuditableEntity : Entity
    {
        public AuditableEntity()
        {
            DateCreated = SystemClock.Now;
        }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}