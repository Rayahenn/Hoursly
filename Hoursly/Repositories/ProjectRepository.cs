using Hoursly.Entities;
using Hoursly.Repositories.Common;

namespace Hoursly.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(HourslyDbContex dbContex) : base(dbContex)
        {
        }
    }
}