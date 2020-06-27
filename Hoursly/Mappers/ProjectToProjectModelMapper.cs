using Hoursly.Entities;
using Hoursly.Mappers.Common;
using Hoursly.Models;

namespace Hoursly.Mappers
{
    public class ProjectToProjectModelMapper : IMapper<Project, ProjectModel>
    {
        public ProjectModel MapFrom(Project source)
        {
            return new ProjectModel(
                source.PublicId,
                source.Name,
                source.StartDate,
                source.SupervisorEmail,
                source.EndDate,
                source.Priority,
                source.TaskLimit);
        }
    }
}