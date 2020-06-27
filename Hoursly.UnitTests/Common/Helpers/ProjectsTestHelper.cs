using System;
using System.Collections.Generic;
using System.Linq;
using Hoursly.Entities;
using Hoursly.Models;

namespace Hoursly.UnitTests.Common.Helpers
{
    public static class ProjectsTestHelper
    {
        private static int _projectNo = 1;

        public static ProjectModel ToProjectModel(this Project fakeValidProject, bool publicIdIsEmpty)
        {
            return new ProjectModel(
                publicIdIsEmpty ? Guid.Empty : fakeValidProject.PublicId,
                fakeValidProject.Name,
                fakeValidProject.StartDate,
                fakeValidProject.SupervisorEmail,
                fakeValidProject.EndDate,
                fakeValidProject.Priority,
                fakeValidProject.TaskLimit);
        }

        public static ProjectModel GetFakeProjectModel()
        {
            var fakeValidProject = GetFakeProjects(1).First();
            var fakeValidProjectModel = fakeValidProject.ToProjectModel(true);
            return fakeValidProjectModel;
        }

        public static IEnumerable<Project> GetFakeProjects(int number)
        {
            var projects = new List<Project>();
            for (var i = 0; i < number; i++)
            {
                var name = "test" + _projectNo++;
                var project = new Project(
                    name,
                    new DateTime(2001, 01, 01),
                    new DateTime(2002, 01, 01),
                    ProjectPriority.Low,
                    10,
                    "test@mail.com");
                projects.Add(project);
            }

            return projects;
        }
    }
}