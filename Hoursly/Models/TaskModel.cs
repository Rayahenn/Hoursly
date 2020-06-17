namespace Hoursly.Models
{
    public class TaskModel
    {
        public TaskModel(
            ProjectModel project,
            string name,
            ProjectPriority priority)
        {
            Project = project;
            Name = name;
            Priority = priority;
        }

        public ProjectModel Project { get; }
        public string Name { get; }
        public ProjectPriority Priority { get; }
    }
}