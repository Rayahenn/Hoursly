using FluentValidation;
using Hoursly.Models;

namespace Hoursly.Validators
{
    public class TimeLogValidator : AbstractValidator<TimeLogModel>
    {
        public TimeLogValidator()
        {
            RuleFor(project => new
                {
                    project.StartDate,
                    project.EndDate
                }).Must(dates => dates.EndDate > dates.StartDate)
                .WithMessage("End date must be greater than Start date");
        }
    }
}