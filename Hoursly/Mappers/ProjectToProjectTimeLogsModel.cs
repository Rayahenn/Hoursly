using System.Linq;
using Hoursly.Entities;
using Hoursly.Mappers.Common;
using Hoursly.Models;

namespace Hoursly.Mappers
{
    public class ProjectToProjectTimeLogsModel : IMapper<Project, ProjectTimeLogsModel>
    {
        public ProjectTimeLogsModel MapFrom(Project source)
        {
            return new ProjectTimeLogsModel(source.PublicId)
            {
                Name = source.Name,
                SupervisorEmail = source.SupervisorEmail,
                TimeLogs = source.TimeLogs.Select(sourceTimeLog =>
                    new TimeLogModel(sourceTimeLog.PublicId, sourceTimeLog.StartDate, sourceTimeLog.EndDate)
                    {
                        StartDate = sourceTimeLog.StartDate,
                        EndDate = sourceTimeLog.EndDate
                    }).ToList()
            };
        }
    }
}