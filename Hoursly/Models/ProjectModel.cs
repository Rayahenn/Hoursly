using System;

namespace Hoursly.Models
{
    public class ProjectModel
    {
        public ProjectModel(
            string name,
            DateTime startDate,
            DateTime? endDate = null,
            ProjectPriority priority = ProjectPriority.Low,
            int? taskLimit = null)
        {
            PublicId = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            TaskLimit = taskLimit;
        }

        public Guid PublicId { get; }
        public string Name { get; }
        public DateTime StartDate { get; }
        public DateTime? EndDate { get; }
        public ProjectPriority Priority { get; }
        public int? TaskLimit { get; }
    }

    public enum ProjectPriority
    {
        Low,
        Medium,
        High
    }
}