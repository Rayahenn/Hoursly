using Hoursly.Entities;
using Hoursly.Models;
using Shouldly;

namespace Hoursly.UnitTests.Common.Helpers
{
    public static class ProjectAssertions
    {
        public static void AssertEqual(this Project left, ProjectModel right)
        {
            left.Name.ShouldBe(right.Name);
            left.StartDate.ShouldBe(right.StartDate);
            left.EndDate.ShouldBe(right.EndDate);
            left.Priority.ShouldBe(right.Priority);
            left.TaskLimit.ShouldBe(right.TaskLimit);
        }

        public static void AssertEqual(this ProjectModel left, ProjectModel right)
        {
            left.PublicId.ShouldBe(right.PublicId);
            left.Name.ShouldBe(right.Name);
            left.StartDate.ShouldBe(right.StartDate);
            left.EndDate.ShouldBe(right.EndDate);
            left.Priority.ShouldBe(right.Priority);
            left.TaskLimit.ShouldBe(right.TaskLimit);
        }
    }
}