using System;
using MediatR;

namespace Hoursly.Application.Projects.Write.Commands
{
    public class ProjectCreateCommand : IRequest<Unit>
    {
        public ProjectCreateCommand(string name, DateTime? deadline)
        {
            Name = name;
            Deadline = deadline;
        }

        public string Name { get; }
        public DateTime? Deadline { get; }
    }
}