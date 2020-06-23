using System;
using System.Collections.ObjectModel;
using Hoursly.Entities;

namespace Hoursly.Models
{
    public class ProjectModel : BaseModel
    {
        public ProjectModel(
            Guid publicId,
            string name,
            DateTime startDate,
            DateTime? endDate = null,
            ProjectPriority priority = ProjectPriority.Low,
            int? taskLimit = null) : base(publicId)
        {
            Tasks = new ObservableCollection<TaskModel>();
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
        private ObservableCollection<TaskModel> Tasks { get; }

        public static ProjectModel Empty()
        {
            return new ProjectModel(
                Guid.Empty,
                string.Empty,
                DateTime.Now);
        }
    }
}