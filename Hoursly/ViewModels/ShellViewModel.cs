using Caliburn.Micro;

namespace Hoursly.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly ProjectsViewModel _projectsViewModel;

        public ShellViewModel(ProjectsViewModel projectsView)
        {
            _projectsViewModel = projectsView;
            ShowProjectsView();
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