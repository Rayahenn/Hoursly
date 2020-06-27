using System;
using FluentValidation;
using Hoursly.Common.Messages;
using Hoursly.Entities;
using Hoursly.Mappers;
using Hoursly.Models;
using Hoursly.UnitTests.Common;
using Hoursly.UnitTests.Common.Helpers;
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
            var mapper = new ProjectToProjectModelMapper();
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
        [Trait("Category", "GetProjects")]
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
        [Trait("Category", "ChangeSelectedProject")]
        public void Given_ChangeSelectedProject_When_ModelPublicIdIsEmpty_Then_EditModeIsTrue()
        {
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();

            _projectsViewModel.SelectedProject = fakeProjectModel;

            _projectsViewModel.EditMode.ShouldBeFalse();
        }

        [Fact]
        [Trait("Category", "ChangeSelectedProject")]
        public void Given_ChangeSelectedProject_When_ModelPublicIdIsNotEmpty_Then_EditModeIsTrue()
        {
            var fakeProjectModel = SeedFakeProject();

            _projectsViewModel.SelectedProject = fakeProjectModel;

            _projectsViewModel.EditMode.ShouldBeTrue();
        }

        [Fact]
        [Trait("Category", "ClearSelection")]
        public void Given_ClearSelection_Then_SelectedProjectShouldBeEmptyAndEditModeShouldBeFalse()
        {
            //Arrange
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();
            _projectsViewModel.SelectedProject = fakeProjectModel;
            _projectsViewModel.EditMode = true;
            var emptyModel = ProjectModel.Empty();

            //Act
            _projectsViewModel.ClearSelection();

            //Assert
            _projectsViewModel.EditMode.ShouldBeFalse();
            _projectsViewModel.SelectedProject.AssertEquals(emptyModel);
        }

        [Fact]
        [Trait("Category", "CreateOrUpdate")]
        public void Given_CreateOrUpdate_When_ModelIsInValidAndEditModeIsFalse_Then_ProjectIsNotCreated()
        {
            //Arrange
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();
            _projectsViewModel.SelectedProject = fakeProjectModel;
            SetupValidatorIsValid(false);
            AssertAreProjectsEmpty();

            //Act
            _projectsViewModel.CreateOrUpdate();

            //Assert
            AssertAreProjectsEmpty();
        }

        [Fact]
        [Trait("Category", "CreateOrUpdate")]
        public void Given_CreateOrUpdate_When_ModelIsInvalidAndEditModeIsTrue_Then_ProjectIsNotUpdated()
        {
            //Arrange
            var notEditedModel = SeedFakeProject();
            _projectsViewModel.SelectedProject = notEditedModel;
            SetupValidatorIsValid(false);

            //Act
            _projectsViewModel.CreateOrUpdate();

            //Assert
            var projectInDb = ProjectRepository.Get(notEditedModel.PublicId);
            projectInDb.AssertEntityEqualsProjectModel(notEditedModel);
        }

        [Fact]
        [Trait("Category", "CreateOrUpdate")]
        public void Given_CreateOrUpdate_When_ModelIsValidAndEditModeIsFalse_Then_ProjectIsCreated()
        {
            //Arrange
            var fakeValidProjectModel = ProjectsTestHelper.GetFakeProjectModel();
            _projectsViewModel.SelectedProject = fakeValidProjectModel;
            SetupValidatorIsValid(true);
            AssertAreProjectsEmpty();

            //Act
            _projectsViewModel.CreateOrUpdate();

            //Assert
            ProjectRepository.GetAll().Count.ShouldBe(1);
        }

        [Fact]
        [Trait("Category", "CreateOrUpdate")]
        public void Given_CreateOrUpdate_When_ModelIsValidAndEditModeIsTrue_Then_ProjectIsUpdated()
        {
            //Arrange
            var editedProjectModel = SeedFakeProject();
            editedProjectModel.Name = "EditedName";
            editedProjectModel.StartDate = new DateTime(2001, 2, 2);
            editedProjectModel.EndDate = null;
            editedProjectModel.Priority = ProjectPriority.High;
            _projectsViewModel.SelectedProject = editedProjectModel;
            SetupValidatorIsValid(true);

            //Act
            _projectsViewModel.CreateOrUpdate();

            //Assert
            var projectInDb = ProjectRepository.Get(editedProjectModel.PublicId);
            projectInDb.AssertEntityEqualsProjectModel(editedProjectModel);
        }

        [Fact]
        [Trait("Category", "DeleteProject")]
        public void Given_Delete_When_ProjectExistInDb_Then_ProjectIsDeleted()
        {
            //Arrange
            var fakeProjectModel = SeedFakeProject();
            _projectsViewModel.SelectedProject = fakeProjectModel;
            ProjectRepository.GetAll().Count.ShouldBe(1);

            //Act
            _projectsViewModel.Delete();

            //Assert
            AssertAreProjectsEmpty();
        }

        [Fact]
        [Trait("Category", "DeleteProject")]
        public void Given_Delete_When_ProjectNoExistInDb_Then_ThrowInvalidOperationException()
        {
            //Arrange
            var noExistingProject = ProjectsTestHelper.GetFakeProjectModel();
            noExistingProject.PublicId = Guid.NewGuid();
            _projectsViewModel.SelectedProject = noExistingProject;

            //Act
            //Assert
            Should.Throw<InvalidOperationException>(() => _projectsViewModel.Delete());
        }

        [Fact]
        [Trait("Category", "GetProjects")]
        public void Given_GetProjects_When_ProjectsNoExistInDb_Then_ReturnsEmptyList()
        {
            var projects = _projectsViewModel.Projects;

            projects.ShouldBeEmpty();
        }
    }
}