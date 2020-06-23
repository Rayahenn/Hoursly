using System;
using Hoursly.Entities;
using Hoursly.Mappers;
using Hoursly.Mappers.Common;
using Hoursly.Models;
using Hoursly.UnitTests.Common.Helpers;
using Xunit;

namespace Hoursly.UnitTests.Mappers
{
    public class ProjectMapperTests
    {
        public ProjectMapperTests()
        {
            _mapper = new ProjectsMapper();
        }

        private readonly IMapper<Project, ProjectModel> _mapper;

        [Fact]
        [Trait("Category", "Mappers")]
        public void Given_ProjectsMapper_ReturnsCorrectProjectModel()
        {
            //Arrange
            var project = new Project(
                "test",
                new DateTime(2011, 2, 2),
                new DateTime(200, 3, 4),
                ProjectPriority.High,
                2);

            //Act
            var projectModel = _mapper.MapFrom(project);

            //Assert
            project.AssertEntityEqualsProjectModel(projectModel);
        }
    }
}