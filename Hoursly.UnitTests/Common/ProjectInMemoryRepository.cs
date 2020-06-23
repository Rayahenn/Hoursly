using Hoursly.Entities;
using Hoursly.Repositories;

namespace Hoursly.UnitTests.Common
{
    public class ProjectInMemoryRepository : InMemoryRepository<Project>, IProjectRepository
    {
    }
}