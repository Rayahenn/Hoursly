using System;
using System.Collections.Generic;
using System.Linq;
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

        private static ProjectModel GetProjectModel(Project fakeValidProject, bool publicIdIsEmpty)
        {
            return new ProjectModel(
                publicIdIsEmpty ? Guid.Empty : fakeValidProject.PublicId,
                fakeValidProject.Name,
                fakeValidProject.StartDate,
                fakeValidProject.EndDate,
                fakeValidProject.Priority,
                fakeValidProject.TaskLimit);
        }

        protected static ProjectModel GetFakeProjectModel()
        {
            var fakeValidProject = GetFakeProjects(1).First();
            var fakeValidProjectModel = GetProjectModel(fakeValidProject, true);
            return fakeValidProjectModel;
        }

        protected ProjectModel SeedFakeProject()
        {
            var fakeValidProject = GetFakeProjects(1).First();
            ProjectRepository.Create(fakeValidProject);
            ProjectRepository.Commit();
            var fakeValidProjectModel = GetProjectModel(fakeValidProject, false);
            return fakeValidProjectModel;
        }

        protected void AssertAreProjectsEmpty()
        {
            ProjectRepository.GetAll().Count.ShouldBe(0);
        }
    }
}