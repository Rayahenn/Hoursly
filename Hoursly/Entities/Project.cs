using System;
using Hoursly.Common.Decorators;

namespace Hoursly.Entities
{
    public class Project : IUniqueIdentifier
    {
        public Project()
        {
            //only for EF
        }

        public Project(
            string name,
            DateTime startDate,
            DateTime? endDate,
            ProjectPriority priority,
            int? taskLimit)
        {
            PublicId = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            TaskLimit = taskLimit;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectPriority Priority { get; set; }
        public int? TaskLimit { get; set; }
        public Guid PublicId { get; set; }

        public static Project Create(
            string name,
            DateTime startDate,
            DateTime? endDate,
            ProjectPriority priority,
            int? taskLimit)
        {
            return new Project(
                name,
                startDate,
                endDate,
                priority,
                taskLimit);
        }

        public void Update(
            string name,
            DateTime startDate,
            DateTime? endDate,
            ProjectPriority priority,
            int? taskLimit)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            TaskLimit = taskLimit;
        }
    }
}