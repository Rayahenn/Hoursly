using System;
using System.Collections.ObjectModel;

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
            Tasks = new ObservableCollection<TaskModel>();
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
        public ObservableCollection<TaskModel> Tasks { get; }
    }

    public enum ProjectPriority
    {
        Low,
        Medium,
        High
    }
}