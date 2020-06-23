using FluentValidation;
using Hoursly.Models;

namespace Hoursly.Validators
{
    public class ProjectValidator : AbstractValidator<ProjectModel>
    {
        public ProjectValidator()
        {
            RuleFor(project => project.Name).NotEmpty().MaximumLength(50);
            RuleFor(project => project.StartDate).NotEmpty();
            RuleFor(project => new
                {
                    project.StartDate,
                    project.EndDate
                }).Must(dates => dates.EndDate.HasValue && dates.EndDate > dates.StartDate)
                .WithMessage("End date must be greater than Start date");
            RuleFor(project => project.TaskLimit).GreaterThan(0);
        }
    }
}