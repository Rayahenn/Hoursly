using Caliburn.Micro;
using Hoursly.Repositories;

namespace Hoursly.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectsViewModel _projectsViewModel;
        public ShellViewModel(IProjectRepository projectRepository, ProjectsViewModel projectsView)
        {
            _projectRepository = projectRepository;
            _projectsViewModel = projectsView;
            ShowReportsView();
        }

        public void ShowReportsView()
        {
            ActivateItem(new ReportsViewModel());
        }

        public void ShowProjectsView()
        {
            ActivateItem(_projectsViewModel);
        }
    }
}