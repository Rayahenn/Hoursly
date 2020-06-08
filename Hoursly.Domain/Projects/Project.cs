using System;
using Hoursly.Domain.Common;

namespace Hoursly.Domain.Projects
{
    public sealed class Project : AuditableEntity
    {
        private Project(
            string name,
            DateTime? deadline)
        {
            Name = name;
            Deadline = deadline;
        }

        public string Name { get; }
        public DateTime? Deadline { get; }

        public static Project Create(
            string name,
            DateTime? deadline)
        {
            return new Project(name, deadline);
        }
    }
}