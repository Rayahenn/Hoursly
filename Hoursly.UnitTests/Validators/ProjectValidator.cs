using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Validators;
using Hoursly.UnitTests.Common.Helpers;
using Hoursly.Validators;
using Shouldly;
using Xunit;

namespace Hoursly.UnitTests.Validators
{
    public class ProjectValidatorTests

    {
        private readonly ProjectValidator _projectValidator = new ProjectValidator();


        public static IEnumerable<object[]> InvalidProjectNames => new List<object[]>
        {
            new object[] {new string('a', 256)},
            new object[] {new string('a', 257)},
            new object[] {new string('a', 258)}
        };

        [Theory]
        [Trait("Category", "ProjectsValidators")]
        [MemberData(nameof(InvalidProjectNames))]
        public void Given_ValidateProject_When_NameIsToLong_Then_ReturnsError(string invalidName)
        {
            //Arrange
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();
            fakeProjectModel.Name = invalidName;

            //Act
            var validationResult = _projectValidator.Validate(fakeProjectModel);

            //Assert
            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.Any(e => e.ErrorCode == nameof(MaximumLengthValidator)).ShouldBeTrue();
        }

        public static IEnumerable<object[]> InvalidEmails => new List<object[]>
        {
            new object[] {new string('a', 30)},
            new object[] {"test.mail.com"},
            new object[] {"@mail.com"}
        };

        [Theory]
        [Trait("Category", "ProjectsValidators")]
        [MemberData(nameof(InvalidEmails))]
        public void Given_ValidateProject_When_SupervisorEmailIsInvalid_Then_ReturnsError(string invalidEmail)
        {
            //Arrange
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();
            fakeProjectModel.SupervisorEmail = invalidEmail;

            //Act
            var validationResult = _projectValidator.Validate(fakeProjectModel);

            //Assert
            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.Any(e => e.ErrorCode == nameof(EmailValidator)).ShouldBeTrue();
        }

        [Fact]
        [Trait("Category", "ProjectsValidators")]
        public void Given_ValidateProject_When_ModelIsCorrect_Then_ReturnsNoError()
        {
            //Arrange
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();

            //Act
            var validationResult = _projectValidator.Validate(fakeProjectModel);

            //Assert
            validationResult.IsValid.ShouldBeTrue();
        }

        [Fact]
        [Trait("Category", "ProjectsValidators")]
        public void Given_ValidateProject_When_NameIsEmpty_Then_ReturnsError()
        {
            //Arrange
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();
            fakeProjectModel.Name = string.Empty;

            //Act
            var validationResult = _projectValidator.Validate(fakeProjectModel);

            //Assert
            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.Any(e => e.ErrorCode == nameof(NotEmptyValidator)).ShouldBeTrue();
        }

        [Fact]
        [Trait("Category", "ProjectsValidators")]
        public void Given_ValidateProject_When_StartDateIsEmpty_Then_ReturnsError()
        {
            //Arrange
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();
            fakeProjectModel.StartDate = DateTime.MinValue;

            //Act
            var validationResult = _projectValidator.Validate(fakeProjectModel);

            //Assert
            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.Any(e => e.ErrorCode == nameof(NotEmptyValidator)).ShouldBeTrue();
        }


        [Fact]
        [Trait("Category", "ProjectsValidators")]
        public void Given_ValidateProject_When_StartDateIsGreaterThanEndDate_Then_ReturnsError()
        {
            //Arrange
            var fakeProjectModel = ProjectsTestHelper.GetFakeProjectModel();
            fakeProjectModel.EndDate = new DateTime(2002, 1, 1);
            fakeProjectModel.StartDate = fakeProjectModel.EndDate.Value.AddDays(1);

            //Act
            var validationResult = _projectValidator.Validate(fakeProjectModel);

            //Assert
            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.Count.ShouldBe(1);
            var error = validationResult.Errors.First();
            error.ErrorMessage.ShouldBe("End date must be greater than Start date");
        }
    }
}