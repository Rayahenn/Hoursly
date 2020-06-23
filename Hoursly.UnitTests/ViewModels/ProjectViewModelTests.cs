using System.Linq;
using FluentValidation;
using Hoursly.Common.Messages;
using Hoursly.Mappers;
using Hoursly.Models;
using Hoursly.UnitTests.Common;
using Hoursly.ViewModels;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Hoursly.UnitTests.ViewModels
{
    public class ProjectViewModelTests : BaseProjectsTest
    {
        public ProjectViewModelTests()
        {
            _projectValidator = Substitute.For<IValidator<ProjectModel>>();
            var systemMessageSender = Substitute.For<ISystemMessageSender>();
            var mapper = new ProjectsMapper();
            _projectsViewModel =
                new ProjectsViewModel(
                    ProjectRepository,
                    mapper,
                    _projectValidator,
                    systemMessageSender
                );
        }

        private readonly ProjectsViewModel _projectsViewModel;
        private readonly IValidator<ProjectModel> _projectValidator;

        private void SetupValidatorIsValid(bool isValid)
        {
            _projectValidator.Validate(Arg.Any<ProjectModel>())
                .Returns(new ValidationResultMock(isValid));
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
        public void Given_CreateProject_When_ModelIsInValidAndEditModeIsFalse_Then_ProjectIsNotCreated()
        {
            //Arrange
            var fakeValidProject = GetFakeProjects(1).First();
            var fakeValidProjectModel = GetProjectModel(fakeValidProject);
            _projectsViewModel.SelectedProject = fakeValidProjectModel;
            SetupValidatorIsValid(false);
            AssertAreProjects();

            //Act
            _projectsViewModel.CreateOrUpdate();

            //Assert
            AssertAreProjects();
        }

        [Fact]
        public void Given_CreateProject_When_ModelIsValidAndEditModeIsFalse_Then_ProjectIsCreated()
        {
            //Arrange
            var fakeValidProject = GetFakeProjects(1).First();
            var fakeValidProjectModel = GetProjectModel(fakeValidProject);
            _projectsViewModel.SelectedProject = fakeValidProjectModel;
            SetupValidatorIsValid(true);
            AssertAreProjects();

            //Act
            _projectsViewModel.CreateOrUpdate();

            //Assert
            ProjectRepository.GetAll().Count.ShouldBe(1);
        }

        [Fact]
        public void Given_GetProjects_When_ProjectsNoExistInDb_Then_ReturnsEmptyList()
        {
            var projects = _projectsViewModel.Projects;

            projects.ShouldBeEmpty();
        }
    }
}