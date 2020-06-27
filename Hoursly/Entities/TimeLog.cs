using System;
using Hoursly.Common.Decorators;

namespace Hoursly.Entities
{
    public class TimeLog : IUniqueIdentifier
    {
        public TimeLog()
        {
            //only for EF
        }

        public TimeLog(Project project,
            DateTime startDate,
            DateTime endDate)
        {
            PublicId = Guid.NewGuid();
            Project = project;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid PublicId { get; set; }

        public static TimeLog Create(
            Project project,
            DateTime startDate,
            DateTime endDate)
        {
            return new TimeLog(project, startDate, endDate);
        }
    }
}