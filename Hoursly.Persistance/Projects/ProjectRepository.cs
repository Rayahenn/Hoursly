using Hoursly.Domain.Projects;
using Hoursly.Persistance.Common;

namespace Hoursly.Persistance.Projects
{
    internal class ProjectRepository : BaseEntityRepository<Project>
    {
        public ProjectRepository(HourslyDbContext contex) : base(contex)
        {
        }
    }
}