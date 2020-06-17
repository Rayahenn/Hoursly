using System;

namespace Hoursly.Models
{
    public class TaskModel
    {
        public TaskModel(
            string name,
            string description,
            ProjectPriority priority,
            TaskStatus status)
        {
            PublicId = Guid.NewGuid();
            Name = name;
            Description = description;
            Priority = priority;
            Status = status;
        }

        public Guid PublicId { get; }
        public string Name { get; }
        public string Description { get; }
        public ProjectPriority Priority { get; }
        public TaskStatus Status { get; }
    }

    public enum TaskStatus
    {
        Todo,
        InProgress,
        Completed
    }
}