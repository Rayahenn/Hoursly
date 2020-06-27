using System;
using System.Collections.Generic;

namespace Hoursly.Models
{
    public class ProjectTimeLogsModel : BaseModel
    {
        public ProjectTimeLogsModel(Guid publicId) : base(publicId)
        {
        }

        public string Name { get; set; }
        public string SupervisorEmail { get; set; }
        public List<TimeLogModel> TimeLogs { get; set; }
    }
}