using System;
using System.Collections.ObjectModel;
using Hoursly.Entities;

namespace Hoursly.Models
{
    public class ProjectModel
    {
        public ProjectModel(
            Guid publicId,
            string name,
            DateTime startDate,
            DateTime? endDate = null,
            ProjectPriority priority = ProjectPriority.Low,
            int? taskLimit = null)
        {
            PublicId = publicId;
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
        private ObservableCollection<TaskModel> Tasks { get; }
    }
}