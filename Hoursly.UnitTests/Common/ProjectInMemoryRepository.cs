using Hoursly.Entities;
using Hoursly.Repositories.Projects;

namespace Hoursly.UnitTests.Common
{
    public class ProjectInMemoryRepository : InMemoryRepository<Project>, IProjectRepository
    {
    }
}