using System;
using System.Collections.Generic;
using Hoursly.Entities;
using Hoursly.Models;
using Hoursly.Repositories;
using Hoursly.UnitTests.Common;
using Shouldly;

namespace Hoursly.UnitTests.ViewModels
{
    public abstract class BaseProjectsTest
    {
        private static int _projectNo = 1;
        protected readonly IProjectRepository ProjectRepository;

        protected BaseProjectsTest()
        {
            ProjectRepository = new ProjectInMemoryRepository();
        }

        protected static IEnumerable<Project> GetFakeProjects(int number)
        {
            var projects = new List<Project>();
            for (var i = 0; i < number; i++)
            {
                var name = "test" + _projectNo++;
                var project = new Project(
                    name,
                    new DateTime(2001, 01, 01),
                    new DateTime(2002, 01, 01),
                    ProjectPriority.Low, 10);
                projects.Add(project);
            }

            return projects;
        }

        protected void SeedFakeProjects(int numberInDb)
        {
            var fakeProjects = GetFakeProjects(numberInDb);
            foreach (var project in fakeProjects)
            {
                ProjectRepository.Create(project);
            }

            ProjectRepository.Commit();
        }

        protected static ProjectModel GetProjectModel(Project fakeValidProject)
        {
            return new ProjectModel(
                Guid.Empty,
                fakeValidProject.Name,
                fakeValidProject.StartDate,
                fakeValidProject.EndDate,
                fakeValidProject.Priority,
                fakeValidProject.TaskLimit);
        }

        protected void AssertAreProjects()
        {
            ProjectRepository.GetAll().Count.ShouldBe(0);
        }
    }
}