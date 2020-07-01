using System;
using System.Collections.Generic;
using System.Linq;
using Hoursly.Common.Decorators;

namespace Hoursly.Entities
{
    public class Project : IUniqueIdentifier
    {
        public Project()
        {
            //only for EF
        }

        public Project(
            string name,
            DateTime startDate,
            DateTime? endDate,
            ProjectPriority priority,
            int? taskLimit,
            string supervisorEmail)
        {
            PublicId = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            TaskLimit = taskLimit;
            SupervisorEmail = supervisorEmail;
            TimeLogs = new HashSet<TimeLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectPriority Priority { get; set; }
        public int? TaskLimit { get; set; }
        public string SupervisorEmail { get; set; }
        public ICollection<TimeLog> TimeLogs { get; set; }
        public Guid PublicId { get; set; }

        public static Project Create(
            string name,
            DateTime startDate,
            DateTime? endDate,
            ProjectPriority priority,
            int? taskLimit,
            string supervisorEmail)
        {
            return new Project(
                name,
                startDate,
                endDate,
                priority,
                taskLimit,
                supervisorEmail);
        }

        public void LogTime(
            DateTime startDate,
            DateTime endDate)
        {
            var timeLog = TimeLog.Create(this, startDate, endDate);
            TimeLogs.Add(timeLog);
        }

        public void RemoveLog(Guid timeLogPublicId)
        {
            var timeLog = TimeLogs.Single(tl => tl.PublicId == timeLogPublicId);
            TimeLogs.Remove(timeLog);
        }

        public void Update(
            string name,
            DateTime startDate,
            DateTime? endDate,
            ProjectPriority priority,
            int? taskLimit,
            string supervisorEmail)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            TaskLimit = taskLimit;
            SupervisorEmail = supervisorEmail;
        }
    }
}