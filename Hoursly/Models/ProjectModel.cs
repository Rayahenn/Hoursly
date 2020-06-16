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
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            TaskLimit = taskLimit;
        }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectPriority Priority { get; set; }
        public int? TaskLimit { get; set; }
    }

    public enum ProjectPriority
    {
        Low,
        Medium,
        High
    }
}