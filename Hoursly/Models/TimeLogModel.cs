using System;

namespace Hoursly.Models
{
    public class TimeLogModel : BaseModel
    {
        public TimeLogModel(Guid publicId,
            DateTime startDate,
            DateTime endDate) : base(publicId)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public static TimeLogModel Empty()
        {
            return new TimeLogModel(
                Guid.Empty,
                DateTime.Now,
                DateTime.Now.AddHours(1));
        }
    }
}