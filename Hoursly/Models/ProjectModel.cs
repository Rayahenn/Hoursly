using System;
using Hoursly.Entities;

namespace Hoursly.Models
{
    public class ProjectModel : BaseModel
    {
        public ProjectModel(
            Guid publicId,
            string name,
            DateTime startDate,
            string supervisorEmail,
            DateTime? endDate = null,
            ProjectPriority priority = ProjectPriority.Low,
            int? taskLimit = null) : base(publicId)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            TaskLimit = taskLimit;
            SupervisorEmail = supervisorEmail;
        }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectPriority Priority { get; set; }
        public int? TaskLimit { get; set; }
        public string SupervisorEmail { get; set; }

        public static ProjectModel Empty()
        {
            return new ProjectModel(
                Guid.Empty,
                string.Empty,
                DateTime.Now,
                string.Empty);
        }
    }
}