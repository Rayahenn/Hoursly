using System.Linq;
using Hoursly.Models;
using Hoursly.Repositories.Projects;
using Hoursly.UnitTests.Common;
using Hoursly.UnitTests.Common.Helpers;
using Shouldly;

namespace Hoursly.UnitTests.ViewModels
{
    public abstract class BaseProjectsTest
    {
        protected readonly IProjectRepository ProjectRepository;

        protected BaseProjectsTest()
        {
            ProjectRepository = new ProjectInMemoryRepository();
        }


        protected void SeedFakeProjects(int numberInDb)
        {
            var fakeProjects = ProjectsTestHelper.GetFakeProjects(numberInDb);
            foreach (var project in fakeProjects)
            {
                ProjectRepository.Create(project);
            }

            ProjectRepository.Commit();
        }


        protected ProjectModel SeedFakeProject()
        {
            var fakeValidProject = ProjectsTestHelper.GetFakeProjects(1).First();
            ProjectRepository.Create(fakeValidProject);
            ProjectRepository.Commit();
            var fakeValidProjectModel = fakeValidProject.ToProjectModel(false);
            return fakeValidProjectModel;
        }

        protected void AssertAreProjectsEmpty()
        {
            ProjectRepository.GetAll().Count.ShouldBe(0);
        }
    }
}