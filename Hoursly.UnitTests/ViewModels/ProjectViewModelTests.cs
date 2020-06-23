using System;
using System.Collections.Generic;
using FluentValidation;
using Hoursly.Entities;
using Hoursly.Mappers;
using Hoursly.Models;
using Hoursly.Repositories;
using Hoursly.UnitTests.Common;
using Hoursly.ViewModels;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Hoursly.UnitTests.ViewModels
{
    public class ProjectViewModelTests
    {
        public ProjectViewModelTests()
        {
            var validator = Substitute.For<IValidator<ProjectModel>>();
            _projectRepository = new ProjectInMemoryRepository();
            _projectsViewModel =
                new ProjectsViewModel(_projectRepository, new ProjectsMapper(), validator);
        }

        private readonly ProjectsViewModel _projectsViewModel;
        private readonly IProjectRepository _projectRepository;

        private static int _projectNo = 1;

        private IEnumerable<Project> GetFakeProjects(int number)
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

        private void SeedFakeProjects(int numberInDb)
        {
            var fakeProjects = GetFakeProjects(numberInDb);
            foreach (var project in fakeProjects)
            {
                _projectRepository.Create(project);
            }

            _projectRepository.Commit();
        }


        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void Given_GetProjects_When_ProjectsExistInDb_Then_ReturnsThoseProjects(int numberInDb)
        {
            //Arrange
            SeedFakeProjects(numberInDb);

            //Act
            var projects = _projectsViewModel.Projects;

            //Assert
            projects.Count.ShouldBe(numberInDb);
            projects.ShouldAllBe(projectModel => projectModel != null);
        }

        [Fact]
        public void Given_CreateProjects_When_ModelIsValid_Then_ProjectIsCreated()
        {
            var projects = _projectsViewModel.Projects;

            projects.ShouldBeEmpty();
        }

        [Fact]
        public void Given_GetProjects_When_NoProjectsInDb_Then_EmptyListIsReturned()
        {
            var projects = _projectsViewModel.Projects;

            projects.ShouldBeEmpty();
        }
    }
}