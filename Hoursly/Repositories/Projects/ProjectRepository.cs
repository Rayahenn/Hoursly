using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Hoursly.Entities;
using Hoursly.Repositories.Common;

namespace Hoursly.Repositories.Projects
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(HourslyDbContex dbContex) : base(dbContex)
        {
        }

        public new List<Project> GetAll()
        {
            return DbContex.Projects
                .Include(c => c.TimeLogs)
                .ToList();
        }

        public new Project Get(Guid publicId)
        {
            return DbContex.Projects
                .Include(c => c.TimeLogs)
                .ToList()
                .SingleOrDefault(c => c.PublicId == publicId);
        }
    }
}