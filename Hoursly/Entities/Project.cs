using System;

namespace Hoursly.Entities
{
    public class Project
    {
        public Project()
        {
            //only for EF
        }

        public Project(
            Guid publicId,
            string name,
            DateTime startDate,
            DateTime? endDate,
            ProjectPriority priority,
            int? taskLimit)
        {
            PublicId = publicId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            TaskLimit = taskLimit;
        }

        public static Project Create(
            Guid publicId,
            string name,
            DateTime startDate,
            DateTime? endDate,
            ProjectPriority priority,
            int? taskLimit)
        {
            return new Project(publicId, name, startDate, endDate, priority, taskLimit);
        }

        public long Id { get; set; }
        public Guid PublicId { get; set; }
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