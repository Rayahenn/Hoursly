using System.Threading;
using System.Threading.Tasks;
using Hoursly.Application.Projects.Write.Commands;
using Hoursly.Domain.Projects;
using MediatR;

namespace Hoursly.Application.Projects.Write.Handlers
{
    public class ProjectCreateHandler : IRequestHandler<ProjectCreateCommand>
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectCreateHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
        {
            var project = Project.Create(request.Name, request.Deadline);
            await _projectRepository.AddAsync(project, cancellationToken);

            return Unit.Value;
        }
    }
}