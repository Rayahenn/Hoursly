using Caliburn.Micro;

namespace Hoursly.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly ProjectsViewModel _projectsViewModel;
        private readonly ReportsViewModel _reportsViewModel;

        public ShellViewModel(ProjectsViewModel projectsView,
            ReportsViewModel reportsViewModel)
        {
            _reportsViewModel = reportsViewModel;
            _projectsViewModel = projectsView;
            ShowProjectsView();
        }


        public void ShowReportsView()
        {
            ActivateItem(_reportsViewModel);
        }

        public void ShowProjectsView()
        {
            ActivateItem(_projectsViewModel);
        }
    }
}